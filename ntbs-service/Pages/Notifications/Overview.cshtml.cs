using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ntbs_service.Helpers;
using ntbs_service.Models;
using ntbs_service.Pages;
using ntbs_service.Services;
using ntbs_service.Models.Enums;

namespace ntbs_service.Pages_Notifications
{
    public class OverviewModel : NotificationModelBase
    {
        public OverviewModel(INotificationService service) : base(service) {}

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Notification = await service.GetNotificationWithAllInfoAsync(id);
            if (Notification == null)
            {
                return NotFound();
            }

            await GetLinkedNotifications();
            NotificationId = Notification.NotificationId;

            if (Notification.NotificationStatus == NotificationStatus.Draft)
            {
                return RedirectToPage("./Edit/Patient", new {id = NotificationId});
            }

            return Page();
        }

        public async Task<IActionResult> OnPostCreateLinkAsync()
        {
            var notification = await service.GetNotificationAsync(NotificationId);
            var linkedNotification = await service.CreateLinkedNotificationAsync(notification);

            return RedirectToPage("/Notifications/Edit/Patient", new {id = linkedNotification.NotificationId});
        }
    }
}