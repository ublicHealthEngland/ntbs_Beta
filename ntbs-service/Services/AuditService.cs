﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFAuditer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ntbs_service.Models.Entities.Alerts;
using ntbs_service.Models.Enums;
using ntbs_service.Models.SeedData;

namespace ntbs_service.Services
{
    public interface IAuditService
    {
        Task AuditNotificationReadAsync(int notificationId, NotificationAuditType auditDetails, string userName);
        Task AuditUnmatchSpecimen(int notificationId,
            string labReferenceNumber,
            string userName,
            NotificationAuditType auditType);
        Task AuditRejectPotentialSpecimen(int notificationId,
            string labReferenceNumber,
            string userName,
            NotificationAuditType auditType);
        Task AuditMatchSpecimen(int notificationId,
            string labReferenceNumber,
            string userName,
            NotificationAuditType auditType);
        Task AuditPrint(int notificationId,
            string userName);
        Task AuditSearch(IQueryCollection queryParameterString, string userName);
        Task<IList<AuditLog>> GetWriteAuditsForNotification(int notificationId);
    }

    public class AuditService : IAuditService
    {
        public const string AuditUserSystem = "SYSTEM";
        private readonly AuditDatabaseContext _auditContext;
        
        public static string SPECIMEN_ENTITY_TYPE = "Specimen";

        private static List<string> ALERT_ENTITY_TYPES = new List<string>
        {
            nameof(DataQualityChildECMLevel),
            nameof(DataQualityClusterAlert),
            nameof(DataQualityDraftAlert),
            nameof(DataQualityTreatmentOutcome12),
            nameof(DataQualityTreatmentOutcome24),
            nameof(DataQualityTreatmentOutcome36),
            nameof(DataQualityBirthCountryAlert),
            nameof(DataQualityClinicalDatesAlert),
            nameof(DataQualityDotVotAlert),
            nameof(DataQualityPotentialDuplicateAlert),
            nameof(MBovisAlert),
            nameof(MdrAlert),
            nameof(UnmatchedLabResultAlert)
        };

        public AuditService(AuditDatabaseContext auditContext)
        {
            _auditContext = auditContext;
        }

        public async Task AuditNotificationReadAsync(int notificationId, NotificationAuditType auditDetails,
            string userName)
        {
            var notificationIdString = notificationId.ToString();
            await _auditContext.AuditOperationAsync(
                notificationIdString,
                RootEntities.Notification,
                auditDetails.ToString(),
                AuditEventType.READ_EVENT,
                userName,
                RootEntities.Notification,
                notificationIdString);
        }

        public async Task AuditUnmatchSpecimen(int notificationId,
            string labReferenceNumber,
            string userName,
            NotificationAuditType auditType)
        {
            await _auditContext.AuditOperationAsync(
                labReferenceNumber,
                SPECIMEN_ENTITY_TYPE,
                auditType.ToString(),
                AuditEventType.UNMATCH_EVENT,
                userName,
                RootEntities.Notification,
                notificationId.ToString());
        }

        public async Task AuditRejectPotentialSpecimen(int notificationId,
            string labReferenceNumber,
            string userName,
            NotificationAuditType auditType)
        {
            await _auditContext.AuditOperationAsync(
                labReferenceNumber,
                SPECIMEN_ENTITY_TYPE,
                auditType.ToString(),
                AuditEventType.REJECT_POTENTIAL,
                userName,
                RootEntities.Notification,
                notificationId.ToString());
        }

        public async Task AuditMatchSpecimen(int notificationId,
            string labReferenceNumber,
            string userName,
            NotificationAuditType auditType)
        {
            await _auditContext.AuditOperationAsync(
                labReferenceNumber,
                SPECIMEN_ENTITY_TYPE,
                auditType.ToString(),
                AuditEventType.MATCH_EVENT,
                userName,
                RootEntities.Notification,
                notificationId.ToString());
        }

        public async Task AuditPrint(int notificationId, string userName)
        {
            await _auditContext.AuditOperationAsync(
                notificationId.ToString(),
                RootEntities.Notification,
                null,
                AuditEventType.PRINT_EVENT,
                userName,
                RootEntities.Notification,
                notificationId.ToString());
        }

        public async Task<IList<AuditLog>> GetWriteAuditsForNotification(int notificationId)
        {
            return await _auditContext.AuditLogs
                .Where(log => log.EventType != AuditEventType.READ_EVENT && log.EventType != AuditEventType.PRINT_EVENT)
                .Where(log => !ALERT_ENTITY_TYPES.Contains(log.EntityType))
                .Where(log => log.RootEntity == RootEntities.Notification)
                .Where(log => log.RootId == notificationId.ToString())
                .ToListAsync();
        }

        public async Task AuditSearch(IQueryCollection queryParameters, string userName)
        {
            var parametersWithValuesDictionary = queryParameters
                .Where(kvp => !string.IsNullOrEmpty(kvp.Value))
                // We are assuming here that all parameters have a single value
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.First());
            var dataForAuditLog = JsonConvert.SerializeObject(parametersWithValuesDictionary);
            await _auditContext.AuditOperationAsync(
                null,
                RootEntities.Notification,
                null,
                AuditEventType.SEARCH_EVENT,
                userName,
                data: dataForAuditLog);
        }
    }
}
