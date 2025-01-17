﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire.Server;
using ntbs_service.DataAccess;
using ntbs_service.DataMigration.Exceptions;
using ntbs_service.Jobs;
using ntbs_service.Models.Entities;
using ntbs_service.Services;
using Sentry;
using Serilog;

namespace ntbs_service.DataMigration
{
    public interface INotificationImportService
    {
        [ExpirationTimeTwoWeeks]
        Task<IList<ImportResult>> ImportByLegacyIdsAsync(PerformContext context, int runId, List<string> legacyIds);

        [ExpirationTimeTwoWeeks]
        Task<IList<ImportResult>> BulkImportByLegacyIdsAsync(PerformContext context, int runId, List<string> legacyIds);

        /// <summary>
        /// Import notifications (and their linked ones) with notification dates in range [rangeStartDate, rangeEndDate)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="runId"></param>
        /// <param name="rangeStartDate"></param>
        /// <param name="rangeEndDate"></param>
        /// <returns></returns>
        [ExpirationTimeTwoWeeks]
        Task<IList<ImportResult>> ImportByDateAsync(PerformContext context, int runId, DateTime rangeStartDate, DateTime
            rangeEndDate);

        [ExpirationTimeTwoWeeks]
        Task<IList<ImportResult>> BulkImportByDateAsync(PerformContext context, int runId, DateTime rangeStartDate, DateTime rangeEndDate);
    }

    public class NotificationImportService : INotificationImportService
    {
        private readonly INotificationMapper _notificationMapper;
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationImportRepository _notificationImportRepository;
        private readonly IImportLogger _logger;
        private readonly IMigratedNotificationsMarker _migratedNotificationsMarker;
        private readonly ISpecimenImportService _specimenImportService;
        private readonly IImportValidator _importValidator;
        private readonly IClusterImportService _clusterImportService;
        private readonly ICultureAndResistanceService _cultureAndResistanceService;
        private readonly IDrugResistanceProfileService _drugResistanceProfileService;
        private readonly ICaseManagerImportService _caseManagerImportService;

        public NotificationImportService(INotificationMapper notificationMapper,
                             INotificationRepository notificationRepository,
                             INotificationImportRepository notificationImportRepository,
                             IImportLogger logger,
                             IHub sentryHub,
                             IMigratedNotificationsMarker migratedNotificationsMarker,
                             ISpecimenImportService specimenImportService,
                             IImportValidator importValidator,
                             IClusterImportService clusterImportService,
                             ICultureAndResistanceService cultureAndResistanceService,
                             IDrugResistanceProfileService drugResistanceProfileService,
                             ICaseManagerImportService caseManagerImportService)
        {
            sentryHub.ConfigureScope(s =>
            {
                s.SetTag("context", "migration");
            });

            _notificationMapper = notificationMapper;
            _notificationRepository = notificationRepository;
            _notificationImportRepository = notificationImportRepository;
            _logger = logger;
            _migratedNotificationsMarker = migratedNotificationsMarker;
            _specimenImportService = specimenImportService;
            _importValidator = importValidator;
            _clusterImportService = clusterImportService;
            _cultureAndResistanceService = cultureAndResistanceService;
            _drugResistanceProfileService = drugResistanceProfileService;
            _caseManagerImportService = caseManagerImportService;
        }

        public async Task<IList<ImportResult>> ImportByDateAsync(PerformContext context, int runId,
            DateTime rangeStartDate, DateTime rangeEndDate)
        {
            _logger.LogInformation(context, runId, "Request to import by Date started");
            _logger.LogInformation(context, runId,
                $"Importing notifications in range from {rangeStartDate.Date} to {rangeEndDate.Date}");

            var notificationsGroupsToImport =
                await _notificationMapper.GetNotificationsGroupedByPatient(context, runId, rangeStartDate,
                    rangeEndDate);

            var importResults = await ImportNotificationGroupsAsync(context, runId, notificationsGroupsToImport);

            _logger.LogInformation(context, runId, "Request to import by Date finished");

            return importResults;
        }

        public async Task<IList<ImportResult>> BulkImportByDateAsync(PerformContext context, int runId,
            DateTime rangeStartDate, DateTime rangeEndDate)
        {
            _notificationImportRepository.AddSystemUserToAudits();
            return await ImportByDateAsync(context, runId, rangeStartDate, rangeEndDate);
        }

        public async Task<IList<ImportResult>> ImportByLegacyIdsAsync(PerformContext context, int runId,
            List<string> legacyIds)
        {
            _logger.LogInformation(context, runId, $"Request to import by Id started for batch group");

            var notificationsGroupsToImport =
                await _notificationMapper.GetNotificationsGroupedByPatient(context, runId, legacyIds);

            var importResults = await ImportNotificationGroupsAsync(context, runId, notificationsGroupsToImport);

            _logger.LogInformation(context, runId, "Request to import by Id finished");

            return importResults;
        }

        public async Task<IList<ImportResult>> BulkImportByLegacyIdsAsync(PerformContext context, int runId,
            List<string> legacyIds)
        {
            _notificationImportRepository.AddSystemUserToAudits();
            return await ImportByLegacyIdsAsync(context, runId, legacyIds);
        }


        private async Task<List<ImportResult>> ImportNotificationGroupsAsync(PerformContext context, int runId,
            IEnumerable<IList<Notification>> notificationsGroups)
        {
            // Filter out notifications that already exist in ntbs database
            var notificationsGroupsToImport = new List<List<Notification>>();
            foreach (var notificationsGroup in notificationsGroups)
            {
                var legacyIds = notificationsGroup.Select(x => x.LegacyId).ToList();
                var legacyIdsToImport = await FilterOutImportedIdsAsync(context, runId, legacyIds);
                if (legacyIdsToImport.Count == notificationsGroup.Count)
                {
                    notificationsGroupsToImport.Add(notificationsGroup.ToList());
                }
                else if (legacyIdsToImport.Count != 0)
                {
                    await _logger.LogGroupError(context, runId, notificationsGroup,
                        "Invalid state. Some notifications already exist in NTBS database. Manual intervention needed");
                }
                // The last option is that ALL notifications in the group were filtered out - that's OK! It means we
                // have already imported this group - this will show up in the import errors.
            }

            var importResults = new List<ImportResult>();
            if (notificationsGroupsToImport.Any())
            {
                // Validate and Import valid notifications
                foreach (var notificationsGroup in notificationsGroupsToImport)
                {
                    importResults.Add(await ValidateAndImportNotificationGroupAsync(context, runId,
                        notificationsGroup));
                }
            }

            return importResults;
        }

        private async Task<ImportResult> ValidateAndImportNotificationGroupAsync(PerformContext context, int runId,
            List<Notification> notifications)
        {
            var importResult = new ImportResult(notifications.First().PatientDetails.FullName);

            _logger.LogInformation(context, runId,
                $"{notifications.Count} notifications found to import in notification group containing legacy ID {notifications.First().LegacyId}");

            // Verify that no repeated NotificationIds have returned
            var ids = notifications.Select(n => n.LegacyId).ToList();
            if (ids.Distinct().Count() != ids.Count)
            {
                importResult.AddGroupError("Aborting group import due to duplicate records found");
                await _logger.LogImportGroupFailure(context, runId, notifications, "due to duplicate records found");
                return importResult;
            }

            var hasAnyRecordAlreadyBeenImported = false;
            foreach (var notification in notifications)
            {
                // Check that record hasn't already been imported
                var foundLtbr = notification.LTBRID is null
                    ? false
                    : await _notificationRepository.NotificationWithLegacyIdExistsAsync(notification.LTBRID);
                var foundEts = notification.ETSID is null
                    ? false
                    : await _notificationRepository.NotificationWithLegacyIdExistsAsync(notification.ETSID);
                
                if (foundEts || foundLtbr)
                {
                    hasAnyRecordAlreadyBeenImported = true;
                    var errorId = foundLtbr ? $"LTBRId = {notification.LTBRID}" : $"ETSId = {notification.ETSID}";
                    var errorMessage = $"A notification has already been imported with {errorId}. " +
                                "Please contact your system administrator to fix this issue.";
                    importResult.AddNotificationError(notification.LegacyId, errorMessage);
                }
            }

            if (hasAnyRecordAlreadyBeenImported)
            {
                await _logger.LogImportGroupFailure(context, runId, notifications, "due to notification having already been imported");
                return importResult;
            }

            var isAnyNotificationInvalid = false;
            foreach (var notification in notifications)
            {
                var linkedNotificationId = notification.LegacyId;
                _logger.LogInformation(context, runId, $"Validating notification with Id={linkedNotificationId}");

                var validationErrors = await _importValidator.CleanAndValidateNotification(context,
                    runId,
                    notification);
                if (!validationErrors.Any())
                {
                    _logger.LogInformation(context, runId, "No validation errors found");
                    importResult.AddValidNotification(linkedNotificationId);
                }
                else
                {
                    isAnyNotificationInvalid = true;
                    importResult.AddValidationErrorsMessages(linkedNotificationId, validationErrors);
                    await _logger.LogNotificationWarning(context, runId, linkedNotificationId,
                        $"has {validationErrors.Count} validation errors");
                    foreach (var validationError in validationErrors)
                    {
                        await _logger.LogNotificationWarning(context, runId, linkedNotificationId, validationError.ErrorMessage);
                    }
                }
            }

            if (isAnyNotificationInvalid)
            {
                await _logger.LogImportGroupFailure(context, runId, notifications, "due to validation errors");
                return importResult;
            }

            _logger.LogSuccess(context, runId, $"Importing {notifications.Count} valid notifications");

            var savedNotifications = await SaveLinkedNotifications(context, runId, importResult, notifications);

            if (savedNotifications != null
                && await MarkNotificationsAsImported(context, runId, importResult, savedNotifications))
            {
                importResult.NtbsIds = savedNotifications.ToDictionary(x => x.LegacyId, x => x.NotificationId);
                await ImportLabResults(context, runId, importResult, savedNotifications);
                await MigrateCultureResistanceSummaries(context, runId, savedNotifications);
                await UpdateDrugResistanceProfiles(context, runId, savedNotifications);
                await UpdateClusterInformation(context, runId, savedNotifications);

                await _logger.LogGroupSuccess(context, runId, notifications);
            }

            return importResult;
        }

        private async Task<List<Notification>> SaveLinkedNotifications(
            PerformContext context,
            int runId,
            ImportResult importResult,
            List<Notification> notifications)
        {
            try
            {
                return await _notificationImportRepository.AddLinkedNotificationsAsync(notifications);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                await _logger.LogImportGroupFailure(context, runId, notifications,
                    "failed to save a notification in the group", e);
                importResult.AddGroupError($"{e.Message}: {e.StackTrace}");
                return null;
            }
        }

        private async Task<bool> MarkNotificationsAsImported(
            PerformContext context,
            int runId,
            ImportResult importResult,
            List<Notification> savedNotifications)
        {
            try
            {
                await _migratedNotificationsMarker.MarkNotificationsAsImportedAsync(savedNotifications);
                return true;
            }
            catch (MarkingNotificationsAsImportedFailedException e)
            {
                Log.Error(e, e.Message);
                await _logger.LogGroupWarning(context, runId, savedNotifications, e.Message);
                importResult.AddGroupError($"{e.Message}: {e.StackTrace}");
                return false;
            }
        }

        private async Task ImportLabResults(
            PerformContext context,
            int runId,
            ImportResult importResult,
            List<Notification> savedNotifications)
        {
            try
            {
                await _specimenImportService.ImportReferenceLabResultsAsync(context, runId, savedNotifications, importResult);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                await _logger.LogGroupWarning(context, runId, savedNotifications,
                    "An error occurred while attempting to import the lab results for the group." +
                    " Please contact your system administrator to fix this issue.");
            }
        }

        private async Task MigrateCultureResistanceSummaries(
            PerformContext context,
            int runId,
            List<Notification> savedNotifications)
        {
            try
            {
                await _cultureAndResistanceService.MigrateNotificationCultureResistanceSummary(savedNotifications);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                await _logger.LogGroupWarning(context, runId, savedNotifications,
                    "An error occurred while attempting to migrate the culture resistance summaries for the group." +
                    " Please contact your system administrator to fix this issue.");
            }
        }

        private async Task UpdateDrugResistanceProfiles(
            PerformContext context,
            int runId,
            List<Notification> savedNotifications)
        {
            try
            {
                await _drugResistanceProfileService.UpdateDrugResistanceProfiles(savedNotifications);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                await _logger.LogGroupWarning(context, runId, savedNotifications,
                    "An error occurred while attempting to update the drug resistance profiles for the group." +
                    " The drug resistance profile update job should be run when the import finishes to fix this issue.");
            }
        }

        private async Task UpdateClusterInformation(
            PerformContext context,
            int runId,
            List<Notification> savedNotifications)
        {
            try
            {
                await _clusterImportService.UpdateClusterInformation(savedNotifications);
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
                await _logger.LogGroupWarning(context, runId, savedNotifications,
                    "An error occurred while attempting to update the cluster information for the group." +
                    " Please contact your system administrator to fix this issue.");
            }
        }

        private async Task<List<string>> FilterOutImportedIdsAsync(PerformContext context, int runId,
            List<string> legacyIds)
        {
            var legacyIdsToImport = new List<string>();
            foreach (var legacyId in legacyIds)
            {
                if (await _notificationRepository.NotificationWithLegacyIdExistsAsync(legacyId))
                {
                    await _logger.LogNotificationWarning(context, runId, legacyId, "already exists in NTBS database");
                }
                else
                {
                    legacyIdsToImport.Add(legacyId);
                }
            }
            return legacyIdsToImport;
        }
    }
}
