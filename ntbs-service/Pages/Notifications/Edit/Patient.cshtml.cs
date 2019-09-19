using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ntbs_service.Helpers;
using ntbs_service.Models;
using ntbs_service.Pages;
using ntbs_service.Services;

namespace ntbs_service.Pages_Notifications
{

    public class PatientModel : ValidationModel
    {
        private readonly INotificationService service;
        private readonly NtbsContext context;
        private readonly IAuditService auditService;

        public SelectList Ethnicities { get; set; }
        public SelectList Countries { get; set; }
        public List<Sex> Sexes { get; set; }

        [BindProperty]
        public PatientDetails Patient { get; set; }
        [BindProperty]
        public FormattedDate FormattedDob { get; set; }
        [BindProperty]
        public int NotificationId { get; set; }


        public PatientModel(INotificationService service, NtbsContext context, IAuditService auditService)
        {
            this.service = service;
            this.context = context;
            this.auditService = auditService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var notification = await service.GetNotificationAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            NotificationId = notification.NotificationId;
            Patient = notification.PatientDetails;

            if (Patient == null)
            {
                Patient = new PatientDetails();
            }

            FormattedDob = Patient.Dob.ConvertToFormattedDate();
            Ethnicities = new SelectList(context.GetAllEthnicitiesAsync().Result, nameof(Ethnicity.EthnicityId), nameof(Ethnicity.Label));
            Countries = new SelectList(context.GetAllCountriesAsync().Result, nameof(Country.CountryId), nameof(Country.Name));
            Sexes = context.GetAllSexesAsync().Result.ToList();

            await auditService.OnGetAuditAsync(notification.NotificationId, Patient);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? NotificationId)
        {
            UpdatePatientFlags();
            SetAndValidateDateOnModel(Patient, nameof(Patient.Dob), FormattedDob);
            
            if (!ModelState.IsValid)
            {
                return await OnGetAsync(NotificationId);
            }

            var notification = await service.GetNotificationAsync(NotificationId);
            await service.UpdatePatientAsync(notification, Patient);
            
            return RedirectToPage("./Episode", new {id = notification.NotificationId});
        }

        private void UpdatePatientFlags() {
            if (Patient.NhsNumberNotKnown) {
                Patient.NhsNumber = null;
                ModelState.Remove("Patient.NhsNumber");
            }

            if (Patient.NoFixedAbode) {
                Patient.Postcode = null;
                ModelState.Remove("Patient.Postcode");
            }
        }

        public ContentResult OnGetValidatePatientProperty(string key, string value)
        {
            return ValidateProperty(new PatientDetails(), key, value);
        }

        public ContentResult OnGetValidatePatientDate(string key, string day, string month, string year)
        {
            return ValidateDate(new PatientDetails(), key, day, month, year);
        }
    }
}