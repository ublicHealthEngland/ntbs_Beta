﻿using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ntbs_service.DataAccess;
using ntbs_service.Helpers;
using ntbs_service.Models.Entities;
using ntbs_service.Models.Validations;
using ntbs_service.Services;

namespace ntbs_service.Pages.ContactDetails
{
    public class EditModel : PageModel
    {
        private readonly IUserRepository _userRepository;
        private readonly ValidationService _validationService;
        private readonly IUserHelper _userHelper;
        private readonly INotificationRepository _notificationRepository;

        public EditModel(IUserRepository userRepository, IUserHelper userHelper, INotificationRepository notificationRepository)
        {
            _userRepository = userRepository;
            _userHelper = userHelper;
            _validationService = new ValidationService(this);
            _notificationRepository = notificationRepository;
        }

        [BindProperty]
        public User ContactDetails { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int UserId { get; set; }
        
        public bool UserHasNotificationAndIsCaseManager { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ContactDetails = await _userRepository.GetUserById(UserId);

            if (UserShouldNotBeAbleToEditDetails())
            {
                return StatusCode((int)HttpStatusCode.Forbidden);
            }

            ContactDetails.CaseManagerTbServices = ContactDetails.CaseManagerTbServices
                .OrderBy(x => x.TbService.Name)
                .ThenBy(x => x.TbService.PHEC.Name)
                .ToList();

            UserHasNotificationAndIsCaseManager = await _notificationRepository.AnyNotificationsForUser(UserId) && ContactDetails.IsCaseManager;

            ContactDetails.JobTitle = ContactDetails.JobTitle.Sanitize();
            ContactDetails.PhoneNumberPrimary = ContactDetails.PhoneNumberPrimary.Sanitize();
            ContactDetails.PhoneNumberSecondary = ContactDetails.PhoneNumberSecondary.Sanitize();
            ContactDetails.EmailPrimary = ContactDetails.EmailPrimary.Sanitize();
            ContactDetails.EmailSecondary = ContactDetails.EmailSecondary.Sanitize();
            ContactDetails.Notes = ContactDetails.Notes.Sanitize();

            return Page();
        }

        public bool IsValid(string key)
        {
            return _validationService.IsValid(key);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (UserShouldNotBeAbleToEditDetails())
            {
                return StatusCode((int)HttpStatusCode.Forbidden);
            }

            var user = await _userRepository.GetUserById(UserId);
            if (user.Username != ContactDetails.Username)
            {
                return StatusCode((int)HttpStatusCode.BadRequest);
            }

            ValidateModel();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _userRepository.UpdateUserContactDetails(ContactDetails);
            return RedirectToPage("/ContactDetails/Index", new {userId = UserId});
        }

        private void ValidateModel()
        {
            TryValidateModel(ContactDetails, nameof(ContactDetails));

            if (!ModelState.IsValid)
            {
                ViewData["EditPageErrorDictionary"] = EditPageValidationErrorGenerator.MapToDictionary(ModelState);
            }
        }

        public ContentResult OnPostValidateCaseManagerProperty([FromBody]InputValidationModel input)
        {
            var user = new User();
            return _validationService.GetPropertyValidationResult(user, input.Key, input.Value);
        }

        private bool UserShouldNotBeAbleToEditDetails()
        {
            return !_userHelper.CurrentUserMatchesUsernameOrIsAdmin(HttpContext, ContactDetails.Username) ||
                   _userHelper.UserIsReadOnly(User);
        }
    }
}
