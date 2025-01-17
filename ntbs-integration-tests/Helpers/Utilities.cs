﻿using System;
using System.Collections.Generic;
using System.Linq;
using ntbs_integration_tests.LabResultsPage;
using ntbs_integration_tests.NotificationPages;
using ntbs_integration_tests.SharingPages;
using ntbs_integration_tests.TransferPages;
using ntbs_service.DataAccess;
using ntbs_service.Models;
using ntbs_service.Models.Entities;
using ntbs_service.Models.Entities.Alerts;
using ntbs_service.Models.Enums;
using ntbs_service.Models.ReferenceEntities;
using ntbs_service.Services;

namespace ntbs_integration_tests.Helpers
{
    public static class Utilities
    {
        #region NotificationIds
        public const int DRAFT_ID = 10001;
        public const int NOTIFIED_ID = 10002;
        public const int DENOTIFIED_ID = 10003;
        public const int NOTIFIED_ID_WITH_NOTIFICATION_DATE = 10004;
        public const int NEW_ID = 10005;
        public const int NOTIFIED_ID_2 = 10006;

        public const int DENOTIFY_WITH_DESCRIPTION = 10010;
        public const int DENOTIFY_NO_DESCRIPTION = 10011;

        public const int DELETE_WITH_DESCRIPTION = 10020;
        public const int DELETE_NO_DESCRIPTION = 10021;

        public const int PATIENT_GROUPED_NOTIFIED_NOTIFICATION_1_SHARED_NHS_NUMBER_1 = 10030;
        public const int PATIENT_GROUPED_NOTIFIED_NOTIFICATION_2_SHARED_NHS_NUMBER_1 = 10031;
        public const int PATIENT_DRAFT_NOTIFICATION_SHARED_NHS_NUMBER_1 = 10032;
        public const int PATIENT_NOTIFIED_NOTIFICATION_SHARED_NHS_NUMBER_1 = 10033;

        public const int PATIENT_NOTIFIED_NOTIFICATION_1_SHARED_NHS_NUMBER_2 = 10034;
        public const int PATIENT_DENOTIFIED_NOTIFICATION_2_SHARED_NHS_NUMBER_2 = 10035;

        public const int NOTIFIED_WITH_TBSERVICE = 10041;
        public const int DRAFT_WITH_TBSERVICE = 10042;
        public const int NOTIFIED_WITH_ACTIVE_HOSPITAL = 10043;
        public const int NOTIFIED_WITH_INACTIVE_CASEMANAGER = 10111;
        public const int NOTIFIED_WITH_ACTIVE_CASEMANAGER_NOT_IN_TB_SERVICE = 10555;
        public const int NOTIFIED_WITH_LEGACY_HOSPITAL = 10044;
        public const int MDR_DETAILS_EXIST = 10050;

        public const int NOTIFICATION_WITH_MANUAL_TESTS = 10051;

        public const int NOTIFICATION_WITH_VENUES = 10060;
        public const int NOTIFICATION_WITH_ADDRESSES = 10061;

        public const int NOTIFICATION_WITH_TREATMENT_EVENTS = 10070;
        public const int NOTIFICATION_FOR_ADD_TREATMENT_OUTCOME = 10071;
        public const int NOTIFICATION_FOR_ADD_TREATMENT_RESTART = 10072;
        public const int DRAFT_WITH_DIAGNOSIS_DATE = 10073;
        public const int DRAFT_WITH_TREATMENT_START_DATE = 10074;
        public const int DRAFT_WITH_NO_START_DATES = 10075;

        public const int CLINICAL_NOTIFICATION_EXTRA_PULMONARY_ID = 10080;
        public const int NOTIFICATION_ID_WITH_CURLY_BRACKETS_IN_ALERT = 10083;
        public const int NOTIFICATION_ID_WITH_CURLY_BRACKETS_IN_TREATMENTEVENT = 10084;
        public const int NOTIFICATION_ID_WITH_CURLY_BRACKETS_IN_TREATMENTEVENTS = 10085;
        public const int NOTIFICATION_ID_WITH_CURLY_BRACKETS_IN_SOCIALCONTEXTVENUES = 10086;
        public const int NOTIFICATION_ID_WITH_CURLY_BRACKETS_IN_SOCIALCONTEXTADDRESSES = 10087;
        public const int NOTIFICATION_ID_WITH_CURLY_BRACKETS_IN_OVERVIEW = 10088;
        public const int NOTIFICATION_ID_WITH_CURLY_BRACKETS_IN_SOCIALCONTEXTVENUE = 10089;
        public const int LATE_DOB_ID = 10090;
        public const int NOTIFIED_ID_WITH_TRANSFER_REQUEST_TO_REJECT = 10091;
        public const int NOTIFICATION_WITH_TRANSFER_REQUEST_TO_ACCEPT = 10092;
        public const int NOTIFICATION_WITH_TRANSFER = 10093;
        public const int NOTIFICATION_ID_WITH_CURLY_BRACKETS_IN_SOCIALCONTEXTADDRESS = 10094;
        public const int NOTIFICATION_DATE_TODAY = 10095;
        public const int NOTIFICATION_DATE_OVER_YEAR_AGO = 10096;
        public const int LINKED_NOTIFICATION_ABINGDON_TB_SERVICE = 10097;
        public const int LINK_NOTIFICATION_ROYAL_FREE_LONDON_TB_SERVICE = 10098;


        public static int SPECIMEN_MATCHING_NOTIFICATION_ID1 = MockSpecimenService.MockSpecimenNotificationId1; // 10100
        public static int SPECIMEN_MATCHING_NOTIFICATION_ID2 = MockSpecimenService.MockSpecimenNotificationId2; // 10101
        public static int SPECIMEN_MATCHING_NOTIFICATION_ID3 = MockSpecimenService.MockSpecimenNotificationId3; // 10102
        public static int SPECIMEN_MATCHING_NOTIFICATION_ID4 = MockSpecimenService.MockSpecimenNotificationId4; // 10103
        public const int SPECIMEN_MATCHING_MANUAL_MATCH_NOTIFICATION_ID = 10104;

        public static string NHS_NUMBER_LEGACY_DUPLICATE = MockMigrationRepository.MockNhsNumberDuplicate; // 9500699141
        public static string NHS_NUMBER_LEGACY_DUPLICATE_SOURCE = MockMigrationRepository.MockNhsNumberDuplicateLegacySource; // 9500699141
        public static string NHS_NUMBER_LEGACY_DUPLICATE_ID = MockMigrationRepository.MockNhsNumberDuplicateLegacyId; // 9500699141

        public const int NOTIFICATION_ID_WITH_MBOVIS_OTHER_CASE_ENTITIES = 10200;
        public const int NOTIFICATION_ID_WITH_MBOVIS_NO_OTHER_CASE_NO_ENTITIES = 10201;
        public const int NOTIFICATION_ID_WITH_MBOVIS_UNKNOWN_OTHER_CASE_NO_ENTITIES = 10202;
        public const int NOTIFICATION_ID_WITH_MBOVIS_NULL_OTHER_CASE_NO_ENTITIES = 10203;
        public const int NOTIFICATION_ID_WITH_MBOVIS_MILK_ENTITIES = 10210;
        public const int NOTIFICATION_ID_WITH_MBOVIS_NO_MILK_NO_ENTITIES = 10211;
        public const int NOTIFICATION_ID_WITH_MBOVIS_UNKNOWN_MILK_NO_ENTITIES = 10212;
        public const int NOTIFICATION_ID_WITH_MBOVIS_NULL_MILK_NO_ENTITIES = 10213;
        public const int NOTIFICATION_ID_WITH_MBOVIS_OCCUPATION_ENTITIES = 10220;
        public const int NOTIFICATION_ID_WITH_MBOVIS_NO_OCCUPATION_NO_ENTITIES = 10221;
        public const int NOTIFICATION_ID_WITH_MBOVIS_UNKNOWN_OCCUPATION_NO_ENTITIES = 10222;
        public const int NOTIFICATION_ID_WITH_MBOVIS_NULL_OCCUPATION_NO_ENTITIES = 10223;
        public const int NOTIFICATION_ID_WITH_MBOVIS_ANIMAL_ENTITIES = 10230;
        public const int NOTIFICATION_ID_WITH_MBOVIS_NO_ANIMAL_NO_ENTITIES = 10231;
        public const int NOTIFICATION_ID_WITH_MBOVIS_UNKNOWN_ANIMAL_NO_ENTITIES = 10232;
        public const int NOTIFICATION_ID_WITH_MBOVIS_NULL_ANIMAL_NO_ENTITIES = 10233;

        public const int CLOSED_NOTIFICATION_IN_ABINGDON = 10310;
        public const int DRAFT_NOTIFICATION_WITH_DRAFT_ALERT = 10311;

        public const int NOTIFICATION_WITH_PREVIOUS_TB_SERVICE_OF_ABINGDON = 10400;

        public const int NOTIFICATION_IN_ABINGDON_TO_SHARE = 10500;
        public const int NOTIFICATION_SHARED_TO_GATESHEAD = 10501;

        public const int UNUSED_NOTIFICATION_ID = 999999;
        #endregion

        #region Alert IDs
        public const int ALERT_ID = 20001;
        public const int TRANSFER_ALERT_ID = 20002;
        public const int TRANSFER_ALERT_ID_TO_ACCEPT = 20003;

        public const int TRANSFER_ALERT_ID_TO_REJECT = 20004;
        public const int TRANSFER_REJECTED_ID = 20005;
        public const int TRANSFER_ALERT_ID_TO_ACCEPT_2 = 20006;
        public const int TRANSFER_ALERT_WITH_DATE = 20010;
        public const int TRANSFER_ALERT_DECLINED = 20011;


        public const int DRAFT_DATA_QUALITY_ALERT = 20007;
        public const int ALERT_TO_DISMISS_ID = 20008;
        public const int CLOSED_ALERT_ID = 20009;
        #endregion

        #region Group IDs
        public const int PATIENT_NOTIFICATION_GROUP_ID = 30001;
        public const int OVERVIEW_NOTIFICATION_GROUP_ID = 30002;
        #endregion

        #region Regions
        public const string NORTH_EAST_REGION_NAME = "North East";
        public const string EAST_MIDLANDS_REGION_NAME = "East Midlands";
        public const string EAST_OF_ENGLAND_REGION_NAME = "East of England";
        public const string SOUTH_EAST_REGION_NAME = "South East";
        public const string LONDON_REGION_NAME = "London";
        public const string NORTH_EAST_PHEC_CODE_GATESHEAD = "E45000009";
        public const string PERMITTED_PHEC_CODE = "E45000019";
        public const string UNPERMITTED_PHEC_CODE = "E45000020";
        #endregion

        #region TB Services
        public const string TBSERVICE_GATESHEAD_AND_SOUTH_TYNESIDE_NAME = "Gateshead and South Tyneside";
        public const string TBSERVICE_GATESHEAD_AND_SOUTH_TYNESIDE_LEGACY_NAME =
            "Gateshead and South Tyneside Legacy TB Service";
        public const string TBSERVICE_ROYAL_DERBY_HOSPITAL_ID = "TBS0181";
        public const string TBSERVICE_ROYAL_FREE_LONDON_TB_SERVICE_ID = "TBS0182";
        public const string TBSERVICE_ABINGDON_COMMUNITY_HOSPITAL_ID = "TBS0001";
        public const string TBSERVICE_GATESHEAD_AND_SOUTH_TYNESIDE_ID = "TBS0076";
        public const string PERMITTED_SERVICE_CODE = "TBS0008";
        public const string UNPERMITTED_SERVICE_CODE = "TBS0009";
        #endregion

        #region Hospitals
        // These IDs match actual reference data - see app db seeding
        public const string HOSPITAL_FULWOOD_HALL_HOSPITAL_ID = "51E00361-B228-4E21-8EFD-06EDD9CBB42C";
        public const string HOSPITAL_FULWOOD_HALL_HOSPITAL_NAME = "FULWOOD HALL HOSPITAL";
        public const string HOSPITAL_ABINGDON_COMMUNITY_HOSPITAL_ID = "93FA0A6C-474D-4AE8-AF23-952076F96336";
        public const string HOSPITAL_ABINGDON_COMMUNITY_HOSPITAL_NAME = "ABINGDON COMMUNITY HOSPITAL";
        public const string HOSPITAL_SOUTH_TYNESIDE_DISTRICT_HOSPITAL_ID = "D3BF3541-5F1E-4BDB-B779-0225FEF56501";
        public const string HOSPITAL_SOUTH_TYNESIDE_DISTRICT_HOSPITAL_NAME = "SOUTH TYNESIDE DISTRICT HOSPITAL";
        public const string HOSPITAL_QUEEN_ELIZABETH_HOSPITAL_GATESHEAD_ID = "65D18006-FE12-4B5B-B781-96A8A8AD877C";
        public const string HOSPITAL_QUEEN_ELIZABETH_HOSPITAL_GATESHEAD_NAME = "QUEEN ELIZABETH HOSPITAL [GATESHEAD]";
        public const string HOSPITAL_DUNSTON_HILL_HOSPITAL_GATESHEAD_ID = "3BE2648F-D5B5-4E8D-880C-AB4DBEB989A6";
        public const string HOSPITAL_FAKE_ID = "f9454382-9fbd-4524-8b65-000000000000";
        #endregion

        #region Users
        public const string CASEMANAGER_ABINGDON_EMAIL = "pheNtbs_nhsUser2@ntbs.phe.com";
        public const string CASEMANAGER_ABINGDON_EMAIL2 = "pheNtbs_nhsUser3@ntbs.phe.com";
        public const int CASEMANAGER_ABINGDON_ID = 1234;
        public const int CASEMANAGER_GATESHEAD_ID1 = 5431;
        public const string CASEMANAGER_GATESHEAD_EMAIL1 = "pheNtbs_nhsUser_gateshead1@ntbs.phe.com";
        public const string CASEMANAGER_GATESHEAD_DISPLAY_NAME1 = "Gateshead User 1";
        public const int CASEMANAGER_GATESHEAD_ID2 = 5432;
        public const string CASEMANAGER_GATESHEAD_EMAIL2 = "pheNtbs_nhsUser_gateshead2@ntbs.phe.com";
        public const string CASEMANAGER_GATESHEAD_DISPLAY_NAME2 = "Gateshead User 2";
        public const int CASEMANAGER_GATESHEAD_INACTIVE_ID = 5433;
        public const string CASEMANAGER_GATESHEAD_INACTIVE_EMAIL = "pheNtbs_nhsUser_gateshead3@ntbs.phe.com";
        public const string CASEMANAGER_GATESHEAD_INACTIVE_DISPLAY_NAME = "Gateshead Inactive User";
        #endregion

        #region AD Groups

        public const string ADMIN_AD_GROUP = "App.Auth.NIS.NTBS.Admin";
        public const string NATIONAL_AD_GROUP = "App.Auth.NIS.NTBS.NTS";
        public const string ABINGDON_AD_GROUP = "App.Auth.NIS.NTBS.Service_Abingdon";
        public const string ASHFORD_AD_GROUP = "App.Auth.NIS.NTBS.Service_Ashford";
        public const string GATESHEAD_AD_GROUP = "App.Auth.NIS.NTBS.Service_Gateshead";
        public const string SOUTH_EAST_AD_GROUP = "App.Auth.NIS.NTBS.SoE";
        public const string READ_ONLY_AD_GROUP = "App.Auth.NIS.NTBS.Read_Only";

        #endregion

        #region Data
        // Below generated by http://danielbayley.uk/nhs-number/
        public const string NHS_NUMBER_SHARED_1 = "6345444995";
        public const string NHS_NUMBER_SHARED_2 = "8809217179";

        public const string PERMITTED_POSTCODE = "TW153AA";
        public const string UNPERMITTED_POSTCODE = "NW51TL";
        #endregion

        public static void SeedDatabase(NtbsContext context)
        {
            // Reference data
            context.PHEC.AddRange(ReferenceDataSeedingHelper.GetPHECList());
            context.TbService.AddRange(ReferenceDataSeedingHelper.GetTBServices());
            context.Hospital.AddRange(ReferenceDataSeedingHelper.GetHospitalsList());
            context.LocalAuthority.AddRange(ReferenceDataSeedingHelper.GetLocalAuthorities());
            context.LocalAuthorityToPhec.AddRange(ReferenceDataSeedingHelper.GetLAtoPHECs());
            context.PostcodeLookup.AddRange(ReferenceDataSeedingHelper.GetPostcodeLookups());

            // General purpose entities shared between tests
            context.User.AddRange(GetCaseManagers());
            context.Notification.AddRange(GetSeedingNotifications());
            context.NotificationGroup.AddRange(GetTestNotificationGroups());
            context.CaseManagerTbService.AddRange(GetCaseManagerTbServicesJoinEntries());
            context.Alert.AddRange(GetSeedingAlerts());
            context.ReleaseVersion.Add(new ReleaseVersion {Version = "test-version", Date = DateTime.UtcNow});

            // Entities required for specific test suites
            context.Notification.AddRange(OverviewPageTests.GetSeedingNotifications());
            context.Notification.AddRange(DenotifyPageTests.GetSeedingNotifications());
            context.Notification.AddRange(DeletePageTests.GetSeedingNotifications());
            context.Notification.AddRange(PatientPageTests.GetSeedingNotifications());
            context.Notification.AddRange(HospitalDetailsPageTests.GetSeedingNotifications());
            context.Notification.AddRange(TransferPageTests.GetSeedingNotifications());
            context.Notification.AddRange(ShareWithServicePageTests.GetSeedingNotifications());
            context.Notification.AddRange(StopShareWithServicePageTests.GetSeedingNotifications());
            context.Notification.AddRange(ManualTestResultEditPagesTests.GetSeedingNotifications());
            context.Notification.AddRange(SocialContextVenueEditPageTests.GetSeedingNotifications());
            context.Notification.AddRange(SocialContextVenuesEditPageTests.GetSeedingNotifications());
            context.Notification.AddRange(SocialContextAddressEditPageTests.GetSeedingNotifications());
            context.Notification.AddRange(SocialContextAddressesEditPageTests.GetSeedingNotifications());
            context.Notification.AddRange(TreatmentEventsEditPageTests.GetSeedingNotifications());
            context.Notification.AddRange(TreatmentEventEditPageTests.GetSeedingNotifications());
            context.Notification.AddRange(ClinicalDetailsPageTests.GetSeedingNotifications());
            context.Notification.AddRange(ActionTransferPageTests.GetSeedingNotifications());
            context.Notification.AddRange(LabResultsPageTests.GetSeedingNotifications());
            context.Notification.AddRange(MBovisExposureToKnownCasesPageTests.GetSeedingNotifications());
            context.Notification.AddRange(MBovisUnpasteurisedMilkConsumptionPageTests.GetSeedingNotifications());
            context.Notification.AddRange(MBovisOccupationExposurePageTests.GetSeedingNotifications());
            context.Notification.AddRange(MBovisAnimalExposurePageTests.GetSeedingNotifications());
            context.Notification.AddRange(DraftEditPageTests.GetSeedingNotifications());
            context.Notification.AddRange(RejectTransferPageTests.GetSeedingNotifications());

            context.TreatmentOutcome.AddRange(TreatmentEventsEditPageTests.GetSeedingOutcomes());

            context.Alert.AddRange(DraftEditPageTests.GetSeedingAlerts());
            context.Alert.AddRange(RejectTransferPageTests.GetSeedingAlerts());
            context.SaveChanges();
        }

        private static IEnumerable<NotificationGroup> GetTestNotificationGroups()
        {
            return new List<NotificationGroup>
            {
                new NotificationGroup { NotificationGroupId = PATIENT_NOTIFICATION_GROUP_ID },
                new NotificationGroup { NotificationGroupId = OVERVIEW_NOTIFICATION_GROUP_ID}
            };
        }

        private static IEnumerable<User> GetCaseManagers()
        {
            return TestUser.GetAll().Select(user => new User
            {
                Id = user.Id,
                Username = user.Username,
                DisplayName = user.DisplayName,
                AdGroups = string.Join(',', user.AdGroups),
                IsCaseManager = true,
                IsReadOnly = user.IsReadOnly,
                IsActive = user.IsActive,
                JobTitle = user.JobTitle,
                PhoneNumberPrimary = user.PhoneNumberPrimary,
                PhoneNumberSecondary = user.PhoneNumberSecondary,
                EmailPrimary = user.EmailPrimary,
                EmailSecondary = user.EmailSecondary,
                Notes = user.Notes
            });
        }

        private static IEnumerable<CaseManagerTbService> GetCaseManagerTbServicesJoinEntries()
        {
            return TestUser.GetAll().SelectMany(user => user.TbServiceCodes.Select(serviceCode =>
                new CaseManagerTbService
                {
                    TbServiceCode = serviceCode,
                    CaseManagerId = user.Id
                }));
        }

        private static IEnumerable<Notification> GetSeedingNotifications()
        {
            return new List<Notification>
            {
                new Notification
                {
                    NotificationId = DRAFT_ID,
                    NotificationStatus = NotificationStatus.Draft,
                    DrugResistanceProfile = new DrugResistanceProfile
                    {
                        DrugResistanceProfileString = "RR/MDR/XDR",
                        Species = "M. bovis"
                    },
                    ClinicalDetails = new ClinicalDetails
                    {
                        TreatmentRegimen = TreatmentRegimen.MdrTreatment
                    }
                },
                new Notification
                {
                    NotificationId = NOTIFIED_ID,
                    NotificationStatus = NotificationStatus.Notified,
                    NotificationDate = new DateTime(2012, 01, 05),
                    // Requires a notification site to pass full validation
                    NotificationSites =
                        new List<NotificationSite>
                        {
                            new NotificationSite {NotificationId = NOTIFIED_ID, SiteId = (int)SiteId.PULMONARY}
                        },
                    HospitalDetails = new HospitalDetails
                    {
                        TBServiceCode = TBSERVICE_ABINGDON_COMMUNITY_HOSPITAL_ID,
                        HospitalId = Guid.Parse(HOSPITAL_ABINGDON_COMMUNITY_HOSPITAL_ID),
                        CaseManagerId = CASEMANAGER_ABINGDON_ID
                    },
                    PatientDetails = new PatientDetails
                    {
                        Dob = new DateTime(1970, 1, 1)
                    },
                    ClinicalDetails = new ClinicalDetails
                    {
                        TreatmentRegimen = TreatmentRegimen.MdrTreatment,
                        DiagnosisDate = new DateTime(2012, 01, 01)
                    },
                    DrugResistanceProfile = new DrugResistanceProfile {Species = "M. bovis"}
                },
                new Notification
                {
                    NotificationId = NOTIFIED_ID_2,
                    NotificationStatus = NotificationStatus.Notified,
                    NotificationSites =
                        new List<NotificationSite>
                        {
                            new NotificationSite {NotificationId = NOTIFIED_ID_2, SiteId = (int)SiteId.PULMONARY}
                        },
                    HospitalDetails = new HospitalDetails
                    {
                        TBServiceCode = PERMITTED_SERVICE_CODE,
                        HospitalId = Guid.Parse(HOSPITAL_ABINGDON_COMMUNITY_HOSPITAL_ID),
                        CaseManagerId = CASEMANAGER_ABINGDON_ID
                    },
                    PatientDetails = new PatientDetails
                    {
                        Dob = new DateTime(1970, 1, 1)
                    },
                    ClinicalDetails = new ClinicalDetails { TreatmentRegimen = TreatmentRegimen.MdrTreatment, DiagnosisDate = new DateTime(2020, 12, 12)},
                },
                new Notification
                {
                    NotificationId = DENOTIFIED_ID,
                    NotificationStatus = NotificationStatus.Denotified,
                    DenotificationDetails = new DenotificationDetails
                    {
                        DateOfDenotification = new DateTime(2020, 12, 25),
                        Reason = DenotificationReason.Other,
                        OtherDescription = "a great reason"
                    },
                    // Requires a notification site to pass full validation
                    NotificationSites = new List<NotificationSite>
                    {
                        new NotificationSite {NotificationId = DENOTIFIED_ID, SiteId = (int)SiteId.PULMONARY}
                    },
                    DrugResistanceProfile = new DrugResistanceProfile
                    {
                        DrugResistanceProfileString = "RR/MDR/XDR",
                        Species = "M. bovis"
                    },
                    ClinicalDetails = new ClinicalDetails
                    {
                        TreatmentRegimen = TreatmentRegimen.MdrTreatment
                    },
                    TreatmentEvents = new List<TreatmentEvent> { new TreatmentEvent
                        {
                            TreatmentEventId = 4444,
                            TreatmentEventType = TreatmentEventType.DiagnosisMade, EventDate = new DateTime(2011, 2, 1)
                        }
                    }
                },
                new Notification
                {
                    NotificationId = MDR_DETAILS_EXIST,
                    NotificationStatus = NotificationStatus.Notified,
                    // Requires a notification site to pass full validation
                    NotificationSites =
                        new List<NotificationSite>
                        {
                            new NotificationSite
                            {
                                NotificationId = MDR_DETAILS_EXIST, SiteId = (int)SiteId.PULMONARY
                            }
                        },
                    MDRDetails = new MDRDetails {ExposureToKnownCaseStatus = Status.Yes, RelationshipToCase = "test"},
                    ClinicalDetails = new ClinicalDetails { TreatmentRegimen = TreatmentRegimen.MdrTreatment }
                }
            };
        }

        private static IEnumerable<Alert> GetSeedingAlerts()
        {
            return new List<Alert>
            {
                new TestAlert
                {
                    AlertId = ALERT_ID,
                    AlertStatus = AlertStatus.Open,
                    CreationDate = DateTime.Now,
                    NotificationId = NOTIFIED_ID,
                    AlertType = AlertType.Test
                },
                new TransferAlert
                {
                    AlertType = AlertType.TransferRequest,
                    AlertId = TRANSFER_ALERT_ID,
                    NotificationId = NOTIFIED_ID,
                    TbServiceCode = PERMITTED_SERVICE_CODE,
                    CaseManagerId = CASEMANAGER_ABINGDON_ID,
                    AlertStatus = AlertStatus.Open
                },
                new TransferAlert
                {
                    AlertType = AlertType.TransferRequest,
                    AlertId = TRANSFER_ALERT_ID_TO_ACCEPT,
                    NotificationId = NOTIFIED_ID_WITH_NOTIFICATION_DATE,
                    TbServiceCode = TBSERVICE_ABINGDON_COMMUNITY_HOSPITAL_ID,
                    CaseManagerId = CASEMANAGER_ABINGDON_ID,
                    AlertStatus = AlertStatus.Open
                },
                new TransferAlert
                {
                    AlertType = AlertType.TransferRequest,
                    AlertId = TRANSFER_ALERT_ID_TO_REJECT,
                    NotificationId = NOTIFIED_ID_WITH_TRANSFER_REQUEST_TO_REJECT,
                    TbServiceCode = TBSERVICE_ABINGDON_COMMUNITY_HOSPITAL_ID,
                    CaseManagerId = CASEMANAGER_ABINGDON_ID,
                    AlertStatus = AlertStatus.Open
                },
                new TransferAlert
                {
                    AlertType = AlertType.TransferRequest,
                    AlertId = TRANSFER_ALERT_ID_TO_ACCEPT_2,
                    NotificationId = NOTIFICATION_WITH_TRANSFER_REQUEST_TO_ACCEPT,
                    TbServiceCode = TBSERVICE_ABINGDON_COMMUNITY_HOSPITAL_ID,
                    CaseManagerId = CASEMANAGER_ABINGDON_ID,
                    AlertStatus = AlertStatus.Open
                },
                new TransferAlert
                {
                    AlertType = AlertType.TransferRequest,
                    AlertId = TRANSFER_ALERT_WITH_DATE,
                    NotificationId = NOTIFIED_WITH_ACTIVE_HOSPITAL,
                    TbServiceCode = TBSERVICE_ABINGDON_COMMUNITY_HOSPITAL_ID,
                    CaseManagerId = CASEMANAGER_ABINGDON_ID,
                    AlertStatus = AlertStatus.Open,
                    TransferDate = new DateTime(2010, 10, 10)
                },
                new TransferRejectedAlert
                {
                    AlertType = AlertType.TransferRejected,
                    AlertId = TRANSFER_REJECTED_ID,
                    NotificationId = NOTIFIED_ID,
                    AlertStatus = AlertStatus.Open
                },
                new DataQualityTreatmentOutcome12
                {
                    AlertId = ALERT_TO_DISMISS_ID,
                    NotificationId = NOTIFIED_ID,
                    AlertStatus = AlertStatus.Open
                },
                new DataQualityTreatmentOutcome12
                {
                    AlertId = CLOSED_ALERT_ID,
                    NotificationId = NOTIFIED_ID,
                    AlertStatus = AlertStatus.Closed
                }
            };
        }

        public static void SetServiceCodeForNotification(NtbsContext context, int notificationId, string code)
        {
            var hospitalDetails = context.HospitalDetails.Find(notificationId);
            hospitalDetails.TBServiceCode = code;
            context.SaveChanges();
        }

        public static void SetPostcodeForNotification(NtbsContext context, int notificationId, string code)
        {
            var patientDetails = context.Patients.Find(notificationId);
            patientDetails.PostcodeToLookup = code;
            context.SaveChanges();
        }
    }
}
