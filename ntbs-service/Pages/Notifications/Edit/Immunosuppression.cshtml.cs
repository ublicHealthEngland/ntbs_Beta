﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ntbs_service.DataAccess;
using ntbs_service.Helpers;
using ntbs_service.Models;
using ntbs_service.Models.Enums;
using ntbs_service.Services;

namespace ntbs_service.Pages.Notifications.Edit
{
    public class ImmunosuppressionModel : NotificationEditModelBase
    {
        [BindProperty]
        public ImmunosuppressionDetails ImmunosuppressionDetails { get; set; }

        public ImmunosuppressionModel(
            INotificationService service,
            IAuthorizationService authorizationService,
            INotificationRepository notificationRepository) : base(service, authorizationService, notificationRepository)
        {
        }

        protected override async Task<IActionResult> PrepareAndDisplayPageAsync(bool isBeingSubmitted)
        {
            ImmunosuppressionDetails = Notification.ImmunosuppressionDetails;
            await SetNotificationProperties(isBeingSubmitted, ImmunosuppressionDetails);

            return Page();
        }

        protected override IActionResult RedirectAfterSaveForDraft(bool isBeingSubmitted)
        {
            return RedirectToPage("./PreviousHistory", new { NotificationId, isBeingSubmitted });
        }

        protected override async Task ValidateAndSave()
        {
            if (ModelState.IsValid)
            {
                await Service.UpdateImmunosuppresionDetailsAsync(Notification, ImmunosuppressionDetails);
            }
        }

        public IActionResult OnGetValidate(
                string status,
                bool hasBioTherapy,
                bool hasTransplantation,
                bool hasOther,
                string otherDescription)
        {
            var parsedStatus = string.IsNullOrEmpty(status) ? null : (Status?)Enum.Parse(typeof(Status), status);
            var model = new ImmunosuppressionDetails
            {
                Status = parsedStatus,
                HasBioTherapy = hasBioTherapy,
                HasTransplantation = hasTransplantation,
                HasOther = hasOther,
                OtherDescription = string.IsNullOrEmpty(otherDescription) ? null : otherDescription
            };

            return ValidationService.ValidateFullModel(model);
        }
    }
}
