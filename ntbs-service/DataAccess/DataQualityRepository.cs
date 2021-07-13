﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ntbs_service.Models.Entities;
using ntbs_service.Models.Entities.Alerts;
using ntbs_service.Models.Enums;
using ntbs_service.Models.QueryEntities;

namespace ntbs_service.DataAccess
{
    public interface IDataQualityRepository
    {
        // Draft Alerts
        Task<IList<Notification>> GetNotificationsEligibleForDqDraftAlertsAsync(int count, int offset);
        Task<int> GetNotificationsEligibleForDqDraftAlertsCountAsync();

        // Birth Country Alerts
        Task<IList<Notification>> GetNotificationsEligibleForDqBirthCountryAlertsAsync(int count, int offset);
        Task<int> GetNotificationsEligibleForDqBirthCountryAlertsCountAsync();

        // Clinical Dates Alerts
        Task<IList<Notification>> GetNotificationsEligibleForDqClinicalDatesAlertsAsync(int count, int offset);
        Task<int> GetNotificationsEligibleForDqClinicalDatesAlertsCountAsync();

        // Cluster Alerts
        Task<IList<Notification>> GetNotificationsEligibleForDqClusterAlertsAsync(int count, int offset);
        Task<int> GetNotificationsEligibleForDqClusterAlertsCountAsync();

        // DOT/VOT Alerts
        Task<IList<Notification>> GetNotificationsEligibleForDqDotVotAlertsAsync(int count, int offset);
        Task<int> GetNotificationsEligibleForDqDotVotAlertsCountAsync();

        // Treatment Outcome 12
        Task<IList<Notification>> GetNotificationsEligibleForDqTreatmentOutcome12AlertsAsync(int count, int offset);
        Task<int> GetNotificationsEligibleForDqTreatmentOutcome12AlertsCountAsync();

        // Treatment Outcome 24
        Task<IList<Notification>> GetNotificationsEligibleForDqTreatmentOutcome24AlertsAsync(int count, int offset);

        Task<int> GetNotificationsEligibleForDqTreatmentOutcome24AlertsCountAsync();

        // Treatment Outcome 36
        Task<IList<Notification>> GetNotificationsEligibleForDqTreatmentOutcome36AlertsAsync(int count, int offset);
        Task<int> GetNotificationsEligibleForDqTreatmentOutcome36AlertsCountAsync();

        // Potential Duplicates
        Task<IList<NotificationAndDuplicateIds>>
            GetNotificationIdsEligibleForDqPotentialDuplicateAlertsAsync();
    }

    public class DataQualityRepository : IDataQualityRepository
    {
        private readonly NtbsContext _context;
        public const int MinNumberDaysNotifiedForAlert = 45;

        public DataQualityRepository(NtbsContext context)
        {
            _context = context;
        }

        #region Draft Alerts

        public Task<IList<Notification>> GetNotificationsEligibleForDqDraftAlertsAsync(int count, int offset) =>
            GetMultipleDraftNotificationsEligibleForAlertsWithOffset<DataQualityDraftAlert>(
                DataQualityDraftAlert.NotificationQualifiesExpression,
                count,
                offset);

        public Task<int> GetNotificationsEligibleForDqDraftAlertsCountAsync() =>
            GetNotificationsEligibleForAlertsCount(DataQualityDraftAlert.NotificationQualifiesExpression);

        #endregion

        #region Birth Country Alerts

        public Task<IList<Notification>> GetNotificationsEligibleForDqBirthCountryAlertsAsync(int count, int offset) =>
            GetMultipleNotificationsEligibleForAlertsWithOffset<DataQualityBirthCountryAlert>(
                DataQualityBirthCountryAlert.NotificationQualifiesExpression,
                count,
                offset);

        public Task<int> GetNotificationsEligibleForDqBirthCountryAlertsCountAsync() =>
            GetNotificationsEligibleForAlertsCount(DataQualityBirthCountryAlert.NotificationQualifiesExpression);

        #endregion

        #region Clinical Dates Alerts

        public Task<IList<Notification>> GetNotificationsEligibleForDqClinicalDatesAlertsAsync(int count, int offset) =>
            GetMultipleNotificationsEligibleForAlertsWithOffset<DataQualityClinicalDatesAlert>(
                DataQualityClinicalDatesAlert.NotificationQualifiesExpression,
                count,
                offset);

        public Task<int> GetNotificationsEligibleForDqClinicalDatesAlertsCountAsync() =>
            GetNotificationsEligibleForAlertsCount(DataQualityClinicalDatesAlert.NotificationQualifiesExpression);

        #endregion

        #region Cluster Alerts

        public Task<IList<Notification>> GetNotificationsEligibleForDqClusterAlertsAsync(int count, int offset) =>
            GetMultipleNotificationsEligibleForAlertsWithOffset<DataQualityClusterAlert>(
                DataQualityClusterAlert.NotificationQualifiesExpression,
                count,
                offset);

        public Task<int> GetNotificationsEligibleForDqClusterAlertsCountAsync() =>
            GetNotificationsEligibleForAlertsCount(DataQualityClusterAlert.NotificationQualifiesExpression);

        #endregion

        #region Dot/Vot Alerts

        public Task<IList<Notification>> GetNotificationsEligibleForDqDotVotAlertsAsync(int count, int offset) =>
            GetMultipleNotificationsEligibleForAlertsWithOffset<DataQualityDotVotAlert>(
                DataQualityDotVotAlert.NotificationQualifiesExpression,
                count,
                offset);

        public Task<int> GetNotificationsEligibleForDqDotVotAlertsCountAsync() =>
            GetNotificationsEligibleForAlertsCount(DataQualityDotVotAlert.NotificationQualifiesExpression);

        #endregion

        #region Treatment Outcomes 12 Months

        public Task<IList<Notification>> GetNotificationsEligibleForDqTreatmentOutcome12AlertsAsync(
            int count,
            int offset) =>
            GetMultipleNotificationsEligibleForDataQualityTreatmentOutcomeAlerts<DataQualityTreatmentOutcome12>(
                DataQualityTreatmentOutcome12.NotificationInQualifyingDateRangeExpression,
                DataQualityTreatmentOutcome12.NotificationInRangeQualifies,
                count,
                offset);

        public Task<int> GetNotificationsEligibleForDqTreatmentOutcome12AlertsCountAsync() =>
            GetNotificationsEligibleForAlertsCount(DataQualityTreatmentOutcome12
                .NotificationInQualifyingDateRangeExpression);

        #endregion

        #region Treatment Outcomes 24 Months

        public Task<IList<Notification>> GetNotificationsEligibleForDqTreatmentOutcome24AlertsAsync(
            int count,
            int offset) =>
            GetMultipleNotificationsEligibleForDataQualityTreatmentOutcomeAlerts<DataQualityTreatmentOutcome24>(
                DataQualityTreatmentOutcome24.NotificationInQualifyingDateRangeExpression,
                DataQualityTreatmentOutcome24.NotificationInRangeQualifies,
                count,
                offset);

        public Task<int> GetNotificationsEligibleForDqTreatmentOutcome24AlertsCountAsync() =>
            GetNotificationsEligibleForAlertsCount(DataQualityTreatmentOutcome24
                .NotificationInQualifyingDateRangeExpression);

        #endregion

        #region Treatment Outcomes 36 Months

        public Task<IList<Notification>> GetNotificationsEligibleForDqTreatmentOutcome36AlertsAsync(
            int count,
            int offset) =>
            GetMultipleNotificationsEligibleForDataQualityTreatmentOutcomeAlerts<DataQualityTreatmentOutcome36>(
                DataQualityTreatmentOutcome36.NotificationInQualifyingDateRangeExpression,
                DataQualityTreatmentOutcome36.NotificationInRangeQualifies,
                count,
                offset);

        public Task<int> GetNotificationsEligibleForDqTreatmentOutcome36AlertsCountAsync() =>
            GetNotificationsEligibleForAlertsCount(DataQualityTreatmentOutcome36
                .NotificationInQualifyingDateRangeExpression);

        #endregion

        private async Task<IList<Notification>> GetMultipleNotificationsEligibleForAlertsWithOffset<T>(
            Expression<Func<Notification, bool>> expression,
            int count,
            int offset) where T : Alert
        {
            return await GetNotificationQueryableForNotifiedDataQualityAlerts<T>()
                .Where(expression)
                .OrderBy(n => n.NotificationId)
                .Skip(offset)
                .Take(count)
                .ToListAsync();
        }

        private async Task<IList<Notification>> GetMultipleDraftNotificationsEligibleForAlertsWithOffset<T>(
            Expression<Func<Notification, bool>> expression,
            int count,
            int offset) where T : Alert
        {
            return await GetDraftNotificationQueryableForDataQualityAlerts<T>()
                .Where(expression)
                .OrderBy(n => n.NotificationId)
                .Skip(offset)
                .Take(count)
                .ToListAsync();
        }

        private async Task<int> GetNotificationsEligibleForAlertsCount(Expression<Func<Notification, bool>> expression)
        {
            return await _context.Notification
                .Where(expression)
                .CountAsync();
        }

        private async Task<IList<Notification>> GetMultipleNotificationsEligibleForDataQualityTreatmentOutcomeAlerts<T>(
            Expression<Func<Notification, bool>> notificationInQualifyingDateRangeExpression,
            Func<Notification, bool> notificationInRangeQualifies,
            int count,
            int offset) where T : Alert
        {
            // IsTreatmentOutcomeMissingAtXYears cannot be translated to SQL so will be calculated in memory so the
            // method has been split up into a DB query and an in memory where statement separated by the ToListAsync call
            var notificationsInDateRange =
                await GetNotificationQueryableForNotifiedTreatmentOutcomeDataQualityAlerts<T>()
                    .Where(notificationInQualifyingDateRangeExpression)
                    .OrderBy(n => n.NotificationId)
                    .Skip(offset)
                    .Take(count)
                    .ToListAsync();
            return notificationsInDateRange
                .Where(notificationInRangeQualifies)
                .ToList();
        }

        public async Task<IList<NotificationAndDuplicateIds>>
            GetNotificationIdsEligibleForDqPotentialDuplicateAlertsAsync()
        {
            var notificationsWithDuplicateFamilyName = await GetNotificationIdsEligibleForDqPotentialDuplicateAlertsBasedOnMatchingFamilyNameAsync();
            var notificationsWithDuplicateNhsNumber = await GetNotificationIdsEligibleForDqPotentialDuplicateAlertsBasedOnMatchingNhsNumberAsync();

            var duplicateNotifications = notificationsWithDuplicateFamilyName;
            foreach (var notification in notificationsWithDuplicateNhsNumber)
            {
                duplicateNotifications.Add(notification);
            }

            return duplicateNotifications;
        }

        private async Task<IList<NotificationAndDuplicateIds>>
            GetNotificationIdsEligibleForDqPotentialDuplicateAlertsBasedOnMatchingFamilyNameAsync()
        {
            // The query that EF generates for this (when we write it in C#) is not fast enough. The problem is the
            // difference in the way that SQL and C# compare nulls. In this case we want the SQL behaviour (where two
            // NULLs are not considered equal). However, EF assumes that we want the C# behaviour (where two NULLs are
            // considered equal). If we explicitly add the null checks, then EF generates an overcomplicated query
            // which runs too slowly.
            // We can change the EF behaviour by setting UseRelationalNulls to false, however this can only be done
            // globally and in most situations we probably want the default behaviour.
            // See https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.infrastructure.relationaldbcontextoptionsbuilder-2.userelationalnulls?view=efcore-5.0
            return await _context.NotificationAndDuplicateIds.FromSqlRaw(@"SELECT N1.[NotificationId] AS [NotificationId], N2.[NotificationId] AS [DuplicateId]
FROM [Notification] AS N1
LEFT JOIN [Patients] AS P1 ON N1.[NotificationId] = P1.[NotificationId]
CROSS JOIN [Notification] AS N2
LEFT JOIN [Patients] AS P2 ON N2.[NotificationId] = P2.[NotificationId]
WHERE 
((P1.[GivenName] = P2.[GivenName] and P1.[FamilyName] = P2.[FamilyName])
OR
(P1.[GivenName] = P2.[FamilyName] and P1.[FamilyName] = P2.[GivenName]))
AND P1.[Dob] = P2.[Dob]
AND P1.[NotificationId] <> P2.[NotificationId]
AND (N1.[NotificationStatus] IN ('Notified', 'Closed')) 
AND (N2.[NotificationStatus] IN ('Notified', 'Closed')) 
AND (N1.[GroupId] <> N2.[GroupId] OR N1.[GroupId] is NULL or N2.[GroupId] is NULL)")
                .ToListAsync();
        }

        private async Task<IList<NotificationAndDuplicateIds>>
            GetNotificationIdsEligibleForDqPotentialDuplicateAlertsBasedOnMatchingNhsNumberAsync()
        {
            return await _context.Notification
                .SelectMany(_ => _context.Notification,
                    (notification, duplicate) => new { notification, duplicate })
                .Where(t =>
                    // Check that one of the following cases are true:
                    // Check that the notification's nhs numbers match, the notification is notified and the notifications
                    // have been notified for at least the minimum amount of time specified
                    (
                        (
                            t.notification.PatientDetails.NhsNumber == t.duplicate.PatientDetails.NhsNumber &&
                            t.notification.PatientDetails.NhsNumber != null
                        )
                        // Check that the notifications are different
                        &&
                        (
                            t.notification.NotificationId != t.duplicate.NotificationId
                        )
                        // Check that the notifications are complete and have not been discarded (denotified or deleted)
                        && (
                            t.notification.NotificationStatus == NotificationStatus.Notified ||
                            t.notification.NotificationStatus == NotificationStatus.Closed
                        )
                        && (
                            t.duplicate.NotificationStatus == NotificationStatus.Notified ||
                            t.duplicate.NotificationStatus == NotificationStatus.Closed
                        )
                        // Check that notifications are not already linked.
                        &&
                        (
                            t.notification.GroupId != t.duplicate.GroupId || t.notification.GroupId == null
                        )
                    )
                )
                .Select(t => new NotificationAndDuplicateIds
                {
                    NotificationId = t.notification.NotificationId,
                    DuplicateId = t.duplicate.NotificationId
                }).ToListAsync();
        }

        private IQueryable<Notification> GetNotificationQueryableForNotifiedDataQualityAlerts<T>() where T : Alert
        {
            return GetBaseNotificationQueryableWithoutAlertWithType<T>()
                .Where(n => n.NotificationStatus == NotificationStatus.Notified)
                .Where(n => n.NotificationDate < DateTime.Now.AddDays(-MinNumberDaysNotifiedForAlert));
        }

        private IQueryable<Notification> GetDraftNotificationQueryableForDataQualityAlerts<T>() where T : Alert
        {
            return GetBaseNotificationQueryableWithoutAlertWithType<T>()
                .Where(DataQualityDraftAlert.NotificationQualifiesExpression);
        }

        private IQueryable<Notification> GetNotificationQueryableForNotifiedTreatmentOutcomeDataQualityAlerts<T>()
            where T : Alert
        {
            return GetBaseNotificationQueryableWithTreatmentEventsForAlerts<T>()
                .Where(n => n.NotificationStatus == NotificationStatus.Notified);
        }

        private IQueryable<Notification> GetBaseNotificationQueryableWithTreatmentEventsForAlerts<T>() where T : Alert
        {
            return GetBaseNotificationQueryableWithoutAlertWithType<T>()
                .Include(n => n.TreatmentEvents)
                .ThenInclude(t => t.TreatmentOutcome);
        }

        // Get notifications which doesn't have any alerts with certain alert type
        private IQueryable<Notification> GetBaseNotificationQueryableWithoutAlertWithType<T>() where T : Alert
        {
            return _context.Notification
                .Include(n => n.HospitalDetails)
                .Include(n => n.Alerts)
                // Only pick a notification which is allowed to have new alerts added
                .Where(n => n.NotificationStatus != NotificationStatus.Closed
                            && n.NotificationStatus != NotificationStatus.Deleted
                            && n.NotificationStatus != NotificationStatus.Denotified)
                // And make sure it doesn't have this type of alert already
                .Where(n => !n.Alerts.Any(a => a is T))
                .OrderBy(n => n.NotificationId)
                .AsSplitQuery();
        }
    }
}
