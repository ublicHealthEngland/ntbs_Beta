﻿using System;
using System.Collections.Generic;
using ntbs_integration_tests.NotificationPages;
using ntbs_service.DataAccess;
using ntbs_service.Models.Entities;
using ntbs_service.Models.Enums;

namespace ntbs_integration_tests.Helpers
{
    public static class Utilities
    {
        public const int ALERT_ID = 1;
        public const int DRAFT_ID = 1;
        public const int NOTIFIED_ID = 2;
        public const int DENOTIFIED_ID = 3;
        public const int NOTIFIED_ID_WITH_NOTIFICATION_DATE = 4;
        public const int NEW_ID = 1000;

        public const int DENOTIFY_WITH_DESCRIPTION = 10;
        public const int DENOTIFY_NO_DESCRIPTION = 11;

        public const int DELETE_WITH_DESCRIPTION = 20;
        public const int DELETE_NO_DESCRIPTION = 21;

        public const int PATIENT_GROUPED_NOTIFIED_NOTIFICATION_SHARED_NHS_NUMBER = 30;
        public const int PATIENT_GROUPED_DENOTIFIED_NOTIFICATION_SHARED_NHS_NUMBER = 31;
        public const int PATIENT_DRAFT_NOTIFICATION_SHARED_NHS_NUMBER = 32;
        public const int PATIENT_NOTIFIED_NOTIFICATION_SHARED_NHS_NUMBER = 33;

        public const int NOTIFIED_WITH_TBSERVICE = 41;
        public const int MDR_DETAILS_EXIST = 50;

        public const int NOTIFICATION_WITH_MANUAL_TESTS = 51;

        public const int NOTIFICATION_GROUP_ID = 1;

        // Below generated by http://danielbayley.uk/nhs-number/
        public const string NHS_NUMBER_SHARED = "6345444995";

      
        // These IDs match actual reference data - see app db seeding
        public const string HOSPITAL_FLEETWOOD_HOSPITAL_ID = "1EE2B39A-428F-44C7-B4BB-000649636591";
        public const string HOSPITAL_ABINGDON_COMMUNITY_HOSPITAL_ID = "93FA0A6C-474D-4AE8-AF23-952076F96336";
        public const string HOSPITAL_FAKE_ID = "f9454382-9fbd-4524-8b65-000000000000";
        public const string TBSERVICE_ROYAL_DERBY_HOSPITAL_ID = "TBS0181";
        public const string TBSERVICE_ROYAL_FREE_LONDON_TB_SERVICE_ID = "TBS0182";
        public const string TBSERVICE_ABINGDON_COMMUNITY_HOSPITAL_ID = "TBS0001";
        public const string PERMITTED_SERVICE_CODE = "TBS0008";
        public const string UNPERMITTED_SERVICE_CODE = "TBS0009";
        public const string PERMITTED_PHEC_CODE = "E45000019";
        public const string UNPERMITTED_PHEC_CODE = "E45000020";
        public const string PERMITTED_POSTCODE = "TW153AA";
        public const string UNPERMITTED_POSTCODE = "NW51TL";
        public const string CASEMANAGER_ABINGDON_EMAIL = "pheNtbs_nhsUser2@ntbs.phe.com";

        public static void SeedDatabase(NtbsContext context)
        {
            // General purpose entities shared between tests
            context.Notification.AddRange(GetSeedingNotifications());
            context.PostcodeLookup.AddRange(GetTestPostcodeLookups());
            context.NotificationGroup.AddRange(GetTestNotificationGroups());
            context.CaseManager.AddRange(GetCaseManagers());
            context.CaseManagerTbService.AddRange(GetCaseManagerTbServicesJoinEntries());
            context.Alert.AddRange(GetSeedingAlerts());

            // Entities required for specific test suites
            context.Notification.AddRange(DenotifyPageTests.GetSeedingNotifications());
            context.Notification.AddRange(DeletePageTests.GetSeedingNotifications());
            context.Notification.AddRange(PatientPageTests.GetSeedingNotifications());
            context.Notification.AddRange(EpisodesPageTests.GetSeedingNotifications());
            context.Notification.AddRange(ManualTestResultEditPagesTests.GetSeedingNotifications());

            context.SaveChanges();
        }

        public static IEnumerable<NotificationGroup> GetTestNotificationGroups()
        {
            return new List<NotificationGroup>
            {
                new NotificationGroup { NotificationGroupId = NOTIFICATION_GROUP_ID }
            };
        }

        // Unlike other reference data, these are not seeded via fluent migrator so we need to add some test postcodes manually
        private static IEnumerable<PostcodeLookup> GetTestPostcodeLookups()
        {
            return new List<PostcodeLookup>
            {
                // Matches permitted PHEC_CODE
                new PostcodeLookup { Postcode = PERMITTED_POSTCODE, LocalAuthorityCode = "E10000030", CountryCode = "E92000001" },
                // Matches unpermitted PHEC_CODE
                new PostcodeLookup { Postcode = UNPERMITTED_POSTCODE, LocalAuthorityCode = "E09000007", CountryCode = "E92000001" }
            };
        }

        private static IEnumerable<CaseManager> GetCaseManagers()
        {
            return new List<CaseManager>
            {
                new CaseManager { Email = CASEMANAGER_ABINGDON_EMAIL, GivenName = "TestCase", FamilyName = "TestManager" }
            };
        }

        private static IEnumerable<CaseManagerTbService> GetCaseManagerTbServicesJoinEntries()
        {
            return new List<CaseManagerTbService>
            {
                new CaseManagerTbService { TbServiceCode = TBSERVICE_ABINGDON_COMMUNITY_HOSPITAL_ID, CaseManagerEmail = CASEMANAGER_ABINGDON_EMAIL }
            };
        }

        public static List<Notification> GetSeedingNotifications()
        {
            return new List<Notification>
            {
                new Notification{ NotificationId = DRAFT_ID, NotificationStatus = NotificationStatus.Draft },
                new Notification
                {
                    NotificationId = NOTIFIED_ID,
                    NotificationStatus = NotificationStatus.Notified,
                    // Requires a notification site to pass full validation
                    NotificationSites = new List<NotificationSite>
                    {
                        new NotificationSite { NotificationId = NOTIFIED_ID, SiteId = (int)SiteId.PULMONARY }
                    }
                },
                new Notification()
                {
                    NotificationId = DENOTIFIED_ID,
                    NotificationStatus = NotificationStatus.Denotified,
                    // Requires a notification site to pass full validation
                    NotificationSites = new List<NotificationSite>
                    {
                        new NotificationSite { NotificationId = DENOTIFIED_ID, SiteId = (int)SiteId.PULMONARY }
                    }
                },
                new Notification()
                {
                    NotificationId = MDR_DETAILS_EXIST,
                    NotificationStatus = NotificationStatus.Notified,
                    // Requires a notification site to pass full validation
                    NotificationSites = new List<NotificationSite>
                    {
                        new NotificationSite { NotificationId = MDR_DETAILS_EXIST, SiteId = (int)SiteId.PULMONARY }
                    },
                    MDRDetails = new MDRDetails { ExposureToKnownCaseStatus = Status.Yes, RelationshipToCase = "test" },
                    ClinicalDetails = new ClinicalDetails { IsMDRTreatment = true }
                }
            };
        }

        public static List<Alert> GetSeedingAlerts()
        {
            return new List<Alert>
            {
                new TestAlert {
                    AlertId = ALERT_ID,
                    AlertStatus = AlertStatus.Open,
                    TbServiceCode = PERMITTED_SERVICE_CODE,
                    CreationDate = DateTime.Now,
                    NotificationId = NOTIFIED_ID,
                    AlertType = AlertType.Test
                }
            };
        }

        public static void SetServiceCodeForNotification(NtbsContext context, int notificationId, string code)
        {
            var notification = context.Notification.Find(notificationId);
            notification.Episode.TBServiceCode = code;
            context.SaveChanges();
        }

        public static void SetPostcodeForNotification(NtbsContext context, int notificationId, string code)
        {
            var notification = context.Notification.Find(notificationId);
            notification.PatientDetails.PostcodeToLookup = code;
            context.SaveChanges();
        }
    }
}
