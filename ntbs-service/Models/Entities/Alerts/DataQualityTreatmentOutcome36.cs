﻿using System;
using System.Linq.Expressions;
using ntbs_service.Helpers;
using ntbs_service.Models.Enums;
using ntbs_service.Services;

namespace ntbs_service.Models.Entities.Alerts
{
    // ReSharper disable once ClassNeverInstantiated.Global - actually instantiated in DataQualityAlertsJob
    public class DataQualityTreatmentOutcome36 : Alert
    {
        public static readonly Expression<Func<Notification, bool>> NotificationInQualifyingDateRangeExpression =
            n => (n.ClinicalDetails.TreatmentStartDate ?? n.NotificationDate) < DateTime.Today.AddYears(-3);

        public static readonly Func<Notification, bool> NotificationInRangeQualifies =
            n => TreatmentOutcomesHelper.IsTreatmentOutcomeMissingAtXYears(n, 3);

        public static readonly Func<Notification, bool> NotificationQualifies = 
            n => NotificationInQualifyingDateRangeExpression.Compile()(n) && NotificationInRangeQualifies(n);
        public override string Action => 
            "Please provide treatment outcome with appropriate date";

        public override string ActionLink =>
            RouteHelper.GetNotificationOverviewPathWithSectionAnchor(
                // ReSharper disable once PossibleInvalidOperationException
                NotificationId.Value,
                NotificationSubPaths.EditTreatmentEvents);

        public DataQualityTreatmentOutcome36()
        {
            AlertType = AlertType.DataQualityTreatmentOutcome36;
        }
    }
}
