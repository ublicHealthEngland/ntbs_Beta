﻿using ExpressiveAnnotations.Attributes;
using ntbs_service.Helpers;
using ntbs_service.Models.Enums;
using ntbs_service.Models.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ntbs_service.Models
{
    public class Notification : ModelBase
    {
        public Notification()
        {
            NotificationStatus = NotificationStatus.Draft;
            PatientDetails = new PatientDetails();
            Episode = new Episode();
            SocialRiskFactors = new SocialRiskFactors();
            ClinicalDetails = new ClinicalDetails();
            PatientTBHistory = new PatientTBHistory();
            ContactTracing = new ContactTracing();
            ImmunosuppressionDetails = new ImmunosuppressionDetails();
            TravelDetails = new TravelDetails();
            VisitorDetails = new VisitorDetails();
            ComorbidityDetails = new ComorbidityDetails();
        }

        [Display(Name = "Notification Id")]
        public int NotificationId { get; set; }
        [MaxLength(50)]
        public string ETSID { get; set; }
        [MaxLength(50)]
        public string LTBRID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? SubmissionDate { get; set; }
        [Display(Name = "Deletion reason")]
        [MaxLength(150)]
        public string DeletionReason { get; set; }

        [Display(Name = "Notification date")]
        [RequiredIf(@"ShouldValidateFull", ErrorMessage = ValidationMessages.FieldRequired)]
        [AssertThat(@"PatientDetails.Dob == null || NotificationDate > PatientDetails.Dob", ErrorMessage = ValidationMessages.NotificationDateShouldBeLaterThanDob)]
        [ValidDateRange(ValidDates.EarliestClinicalDate)]
        public DateTime? NotificationDate { get; set; }
        public NotificationStatus NotificationStatus { get; set; }

        public virtual PatientDetails PatientDetails { get; set; }
        public virtual ClinicalDetails ClinicalDetails { get; set; }

        [AssertThat("NotificationSites.Count > 0 || !ShouldValidateFull", ErrorMessage = ValidationMessages.DiseaseSiteIsRequired)]
        public virtual List<NotificationSite> NotificationSites { get; set; }
        public virtual Episode Episode { get; set; }
        public virtual PatientTBHistory PatientTBHistory { get; set; }
        public virtual ContactTracing ContactTracing { get; set; }
        public virtual SocialRiskFactors SocialRiskFactors { get; set; }
        public virtual ImmunosuppressionDetails ImmunosuppressionDetails { get; set; }
        public virtual TravelDetails TravelDetails { get; set; }
        public virtual VisitorDetails VisitorDetails { get; set; }
        public virtual DenotificationDetails DenotificationDetails { get; set; }
        public virtual ComorbidityDetails ComorbidityDetails { get; set; }
        public int? GroupId { get; set; }
        public virtual NotificationGroup Group { get; set; }

        public string NotificationStatusString => GetNotificationStatusString();
        [Display(Name = "Date notified")]
        public string FormattedSubmissionDate => FormatDate(SubmissionDate);
        public string FullName => string.Join(", ", new[] { PatientDetails.FamilyName?.ToUpper(), PatientDetails.GivenName }.Where(s => !String.IsNullOrEmpty(s)));
        public string SexLabel => PatientDetails.Sex?.Label;
        public string EthnicityLabel => PatientDetails.Ethnicity?.Label;
        public string CountryName => PatientDetails.Country?.Name;
        [Display(Name = "TB Service")]
        public string TBServiceName => Episode.TBService?.Name;
        public string HospitalName => Episode.Hospital?.Name;
        public string IsPostMortemYesNo => TrueFalseToYesNo(ClinicalDetails.IsPostMortem);
        public string IsSymptomatic => TrueFalseToYesNo(ClinicalDetails.IsSymptomatic);
        public string NoSampleTakenYesNo => TrueFalseToYesNo(!ClinicalDetails.NoSampleTaken);
        public string NotPreviouslyHadTBYesNo => TrueFalseToYesNo(!PatientTBHistory.NotPreviouslyHadTB);
        public string UkBornYesNo => TrueFalseToYesNo(PatientDetails.UkBorn);
        public string IsShortCourseYesNo => TrueFalseToYesNo(ClinicalDetails.IsShortCourseTreatment);
        public string HasRecentVisitor => TrueFalseToYesNo(VisitorDetails.HasVisitor);
        public string HasRecentTravel => TrueFalseToYesNo(TravelDetails.HasTravel);
        public string FormattedNhsNumber => FormatNhsNumberString();
        public IList<string> FormattedAddress => (PatientDetails.Address ?? string.Empty).Split(Environment.NewLine);
        public string FormattedNoAbodeOrPostcodeString => CreateNoAbodeOrPostcodeString();
        public string FormattedOccupationString => PatientDetails?.FormatOccupationString();
        public string SitesOfDiseaseList => CreateSitesOfDiseaseString();
        public string DrugRiskFactorTimePeriods => CreateTimePeriodsString(SocialRiskFactors.RiskFactorDrugs);
        public string HomelessRiskFactorTimePeriods => CreateTimePeriodsString(SocialRiskFactors.RiskFactorHomelessness);
        public string ImprisonmentRiskFactorTimePeriods => CreateTimePeriodsString(SocialRiskFactors.RiskFactorImprisonment);
        public int? DaysFromOnsetToTreatment => CalculateDaysBetweenNullableDates(ClinicalDetails.TreatmentStartDate, ClinicalDetails.SymptomStartDate);
        public int? DaysFromOnsetToFirstPresentation => CalculateDaysBetweenNullableDates(ClinicalDetails.FirstPresentationDate, ClinicalDetails.SymptomStartDate);
        public int? DaysFromFirstPresentationToTBServicePresentation => CalculateDaysBetweenNullableDates(ClinicalDetails.TBServicePresentationDate, ClinicalDetails.FirstPresentationDate);
        public int? DaysFromTBServicePresentationToDiagnosis => CalculateDaysBetweenNullableDates(ClinicalDetails.DiagnosisDate, ClinicalDetails.TBServicePresentationDate);
        public int? DaysFromDiagnosisToTreatment => CalculateDaysBetweenNullableDates(ClinicalDetails.TreatmentStartDate, ClinicalDetails.DiagnosisDate);
        public string BCGVaccinationStateAndYear => FormatStateAndYear(ClinicalDetails.BCGVaccinationState, ClinicalDetails.BCGVaccinationYear);
        public string MDRTreatmentStateAndDate => FormatBooleanStateAndDate(ClinicalDetails.IsMDRTreatment, ClinicalDetails.MDRTreatmentStartDate);
        public string FormattedSymptomStartDate => FormatDate(ClinicalDetails.SymptomStartDate);
        public string FormattedPresentationToAnyHealthServiceDate => FormatDate(ClinicalDetails.FirstPresentationDate);
        public string FormattedPresentationToTBServiceDate => FormatDate(ClinicalDetails.TBServicePresentationDate);
        public string FormattedDiagnosisDate => FormatDate(ClinicalDetails.DiagnosisDate);
        public string FormattedTreatmentStartDate => FormatDate(ClinicalDetails.TreatmentStartDate);
        public string FormattedDeathDate => FormatDate(ClinicalDetails.DeathDate);
        public string FormattedDob => FormatDate(PatientDetails.Dob);
        [Display(Name = "Date created")]
        public string FormattedCreationDate => FormatDate(CreationDate);
        public string FormattedNotificationDate => FormatDate(NotificationDate);
        public string HIVTestState => ClinicalDetails.HIVTestState?.GetDisplayName() ?? string.Empty;

        public string LocalAuthorityName => PatientDetails?.PostcodeLookup?.LocalAuthority?.Name;
        public string ResidencePHECName => PatientDetails?.PostcodeLookup?.LocalAuthority?.LocalAuthorityToPHEC?.PHEC?.Name;
        public string TreatmentPHECName => Episode.TBService?.PHEC?.Name;

        public int? AgeAtNotification => GetAgeAtTimeOfNotification();

        private string GetNotificationStatusString()
        {
            if (NotificationStatus == NotificationStatus.Draft)
            {
                return "Draft";
            }
            else if (NotificationStatus == NotificationStatus.Notified)
            {
                return "Notification";
            }
            else if (NotificationStatus == NotificationStatus.Denotified)
            {
                return "Denotified";
            }

            throw new InvalidOperationException("Notification status is not currently set");
        }

        private string FormatDate(DateTime? date)
        {
            return date?.ToString("dd-MMM-yyyy");
        }

        private string TrueFalseToYesNo(bool? x)
        {
            if (x == null)
            {
                return string.Empty;
            }
            else
            {
                return x.Value ? "Yes" : "No";
            }
        }

        private string FormatStateAndYear(Status? state, int? year)
        {
            return state?.ToString() + (year != null ? " - " + year : string.Empty);
        }

        private string FormatBooleanStateAndDate(bool? booleanState, DateTime? date)
        {
            return TrueFalseToYesNo(booleanState) + (date != null ? " - " + FormatDate(date) : string.Empty);
        }

        private int? CalculateDaysBetweenNullableDates(DateTime? date1, DateTime? date2)
        {
            return (date1?.Date - date2?.Date)?.Days;
        }

        private string CreateSitesOfDiseaseString()
        {
            if (NotificationSites == null)
            {
                return string.Empty;
            }

            var siteNames = NotificationSites.Select(ns => ns.Site)
                .Where(ns => ns != null)
                .Select(s => s.Description);
            return string.Join(", ", siteNames);
        }

        private string CreateNoAbodeOrPostcodeString()
        {
            if (PatientDetails.NoFixedAbode)
            {
                return "No fixed abode";
            }
            else
            {
                return PatientDetails.Postcode?.Trim();
            }
        }

        private string FormatNhsNumberString()
        {
            if (PatientDetails.NhsNumberNotKnown)
            {
                return "Not known";
            }
            if (string.IsNullOrEmpty(PatientDetails.NhsNumber))
            {
                return string.Empty;
            }
            return string.Join(" ",
                PatientDetails.NhsNumber.Substring(0, 3),
                PatientDetails.NhsNumber.Substring(3, 3),
                PatientDetails.NhsNumber.Substring(6, 4)
            );
        }

        private string CreateTimePeriodsString(RiskFactorDetails riskFactor)
        {
            var timeStrings = new List<string>();
            if (riskFactor.IsCurrent)
            {
                timeStrings.Add("current");
            }
            if (riskFactor.InPastFiveYears)
            {
                timeStrings.Add("less than 5 years ago");
            }
            if (riskFactor.MoreThanFiveYearsAgo)
            {
                timeStrings.Add("more than 5 years ago");
            }
            return string.Join(", ", timeStrings);
        }

        private int? GetAgeAtTimeOfNotification()
        {
            if (NotificationDate == null || PatientDetails?.Dob == null)
            {
                return null;
            }

            var notificationDate = (DateTime)NotificationDate;
            var birthDate = (DateTime)PatientDetails.Dob;

            var yearDiff = notificationDate.Year - birthDate.Year;
            if ((birthDate.Month * 100 + birthDate.Day) > (notificationDate.Month * 100 + notificationDate.Day))
            {
                yearDiff -= 1;
            }
            return yearDiff;
        }
    }
}
