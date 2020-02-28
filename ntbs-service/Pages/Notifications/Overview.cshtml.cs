﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ntbs_service.DataAccess;
using ntbs_service.Helpers;
using ntbs_service.Models.Entities;
using ntbs_service.Models.Enums;
using ntbs_service.Models.ReferenceEntities;
using ntbs_service.Services;

namespace ntbs_service.Pages.Notifications
{
    public class OverviewModel : NotificationModelBase
    {
        private readonly IAlertService _alertService;
        private readonly ICultureAndResistanceService _cultureAndResistanceService;
        private readonly ITreatmentOutcomeService _treatmentOutcomeService;
        
        public CultureAndResistance CultureAndResistance { get; set; }
        public Dictionary<int, List<TreatmentEvent>> GroupedTreatmentEvents { get; set; }
        
        public TreatmentOutcome OutcomeAt12Month { get; set; }
        public TreatmentOutcome OutcomeAt24Month { get; set; }
        public TreatmentOutcome OutcomeAt36Month { get; set; }
        
        public OverviewModel(
            INotificationService service,
            IAuthorizationService authorizationService,
            IAlertService alertService,
            INotificationRepository notificationRepository,
            ICultureAndResistanceService cultureAndResistanceService,
            ITreatmentOutcomeService treatmentOutcomeService) : base(service, authorizationService, notificationRepository)
        {
            _alertService = alertService;
            _cultureAndResistanceService = cultureAndResistanceService;
            _treatmentOutcomeService = treatmentOutcomeService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Notification = await NotificationRepository.GetNotificationWithAllInfoAsync(NotificationId);
            if (Notification == null)
            {
                return NotFound();
            }
            NotificationId = Notification.NotificationId;

            await GetLinkedNotifications();
            await GetAlertsAsync();
            await AuthorizeAndSetBannerAsync();
            if (PermissionLevel == PermissionLevel.None)
            {
                return Partial("./UnauthorizedWarning", this);
            }

            // This check has to happen after authorization as otherwise patient will redirect to overview and we'd be stuck in a loop.
            if (Notification.NotificationStatus == NotificationStatus.Draft)
            {
                return RedirectToPage("./Edit/PatientDetails", new { NotificationId });
            }

            CultureAndResistance = await _cultureAndResistanceService.GetCultureAndResistanceDetailsAsync(NotificationId);
            GroupedTreatmentEvents = Notification.TreatmentEvents.GroupByEpisode();

            CalculateTreatmentOutcomes();
            return Page();
        }

        private void CalculateTreatmentOutcomes()
        {
            OutcomeAt12Month = _treatmentOutcomeService.GetTreatmentOutcomeAtXYears(Notification, 1);
            OutcomeAt24Month = _treatmentOutcomeService.GetTreatmentOutcomeAtXYears(Notification, 2);
            OutcomeAt36Month = _treatmentOutcomeService.GetTreatmentOutcomeAtXYears(Notification, 3);
        }

        public async Task<IActionResult> OnPostCreateLinkAsync()
        {
            var notification = await NotificationRepository.GetNotificationAsync(NotificationId);
            var linkedNotification = await Service.CreateLinkedNotificationAsync(notification, User);

            return RedirectToPage("/Notifications/Edit/PatientDetails", new { linkedNotification.NotificationId });
        }

        public async Task GetAlertsAsync()
        {
            Alerts = await _alertService.GetAlertsForNotificationAsync(NotificationId, User);
        }
    }
}
