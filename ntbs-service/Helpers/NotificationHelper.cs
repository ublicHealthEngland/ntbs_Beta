﻿using System;
using System.Linq;
using ntbs_service.Models.Entities;
using ntbs_service.Models.Enums;

namespace ntbs_service.Helpers
{
    public static class NotificationHelper
    {
        public static void SetShouldValidateFull(Notification notification)
        {
            notification.ShouldValidateFull = true;
            foreach (var property in notification.GetType().GetProperties())
            {
                if (property.PropertyType.IsSubclassOf(typeof(ModelBase)))
                {
                    var ownedModel = property.GetValue(notification);
                    ownedModel.GetType().GetProperty("ShouldValidateFull").SetValue(ownedModel, true);
                }
            }

            notification.NotificationSites?.ForEach(site => site.ShouldValidateFull = notification.ShouldValidateFull);
        }

        public static TreatmentEvent CreateTreatmentStartEvent(Notification notification)
        {
            return CreateTreatmentStartEvent(
                notification,
                notification.HospitalDetails.CaseManagerId,
                notification.HospitalDetails.TBServiceCode);
        }

        public static TreatmentEvent CreateTreatmentStartEvent(
            Notification notification,
            int? caseManagerId,
            string tbServiceCode)
        {
            var treatmentStartEvent = new TreatmentEvent
            {
                NotificationId = notification.NotificationId,
                CaseManagerId = caseManagerId,
                TbServiceCode = tbServiceCode
            };
            SetStartingEventDateAndType(treatmentStartEvent, notification.ClinicalDetails);
            return treatmentStartEvent;
        }

        // The two events of TreatmentStart and Diagnosis are somewhat interchangeable, as Diagnosis is the fallback
        // "starting event" when no treatment start is recorded.
        // We need two distinct types so the labels are correct though!
        // We don't want both events in, since we want the outcome dates to be based off of treatment start
        public static void SetStartingEventDateAndType(TreatmentEvent treatmentStartEvent, ClinicalDetails clinicalDetails)
        {
            treatmentStartEvent.EventDate = clinicalDetails.StartingDate;
            treatmentStartEvent.TreatmentEventType = clinicalDetails.TreatmentStartDate != null
                ? TreatmentEventType.TreatmentStart
                : TreatmentEventType.DiagnosisMade;
        }
        
        public static DateTime? Earliest(params DateTime?[] dates)
        {
            return dates.Where(d => d.HasValue).OrderBy(d => d).FirstOrDefault();
        }
    }
}
