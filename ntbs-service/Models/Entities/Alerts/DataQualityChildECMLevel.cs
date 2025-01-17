﻿using System;
using System.Linq.Expressions;
using EFAuditer;
using ntbs_service.Helpers;
using ntbs_service.Models.Enums;

namespace ntbs_service.Models.Entities.Alerts

{
    public class DataQualityChildECMLevel : Alert, IHasRootEntityForAuditing
    {
        private const int MinNumberDaysDraftForAlert = 45;

        public static readonly Expression<Func<Notification, bool>> NotificationQualifiesExpression =
            n => n.NotificationStatus == NotificationStatus.Notified
                 && n.NotificationDate < DateTime.Now.AddDays(-MinNumberDaysDraftForAlert)
                 && n.NotificationDate.Value < n.PatientDetails.Dob.Value.AddYears(16)
                 && n.ClinicalDetails.EnhancedCaseManagementLevel == 0;

        public static readonly Func<Notification, bool> NotificationQualifies =
            NotificationQualifiesExpression.Compile();

        public override string Action => "Please review whether the Enhanced Case Management information is correct.";

        public override string ActionLink => RouteHelper.GetNotificationPath(
            NotificationId.GetValueOrDefault(),
            NotificationSubPaths.EditClinicalDetails);

        public DataQualityChildECMLevel()
        {
            AlertType = AlertType.DataQualityChildECMLevel;
        }
        public string RootEntityType => RootEntities.Notification;
        public string RootId => NotificationId.ToString();
    }
}
