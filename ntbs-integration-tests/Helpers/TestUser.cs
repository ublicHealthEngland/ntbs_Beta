﻿using System.Collections.Generic;
using ntbs_service.Models.Enums;

namespace ntbs_integration_tests.Helpers
{
    public interface ITestUser
    {
        string Username { get; }
        string DisplayName { get; }
        UserType Type { get; }
        IEnumerable<string> AdGroups { get; }
        // When setting the TbServiceCodes, make sure that the user's also have the corresponding AD groups,
        // as defined by the seed data in tbservice.csv. Otherwise, they still won't have access to the
        // TB Services.
        IEnumerable<string> TbServiceCodes { get; }
        bool IsReadOnly { get; }
    }

    // Tech debt: Some of the AD groups which are hardcoded here should actually be taken from config.
    public class TestUser : ITestUser
    {
        public int Id { get; }
        public string Username { get; }
        public string DisplayName { get; }
        public UserType Type { get; }
        public IEnumerable<string> AdGroups { get; }
        public IEnumerable<string> TbServiceCodes { get; }
        public bool IsReadOnly { get; }
        public bool IsActive { get; }
        public string JobTitle { get; set; }
        public string EmailPrimary { get; set; }
        public string EmailSecondary { get; set; }
        public string PhoneNumberPrimary { get; set; }
        public string PhoneNumberSecondary { get; set; }
        public string Notes { get; set; }

        private TestUser(
            int id,
            string username,
            string displayName,
            UserType type,
            IEnumerable<string> adGroups,
            IEnumerable<string> tbServiceCodes = null,
            bool isReadOnly = false,
            bool isActive = true,
            string jobTitle = null,
            string emailPrimary = null,
            string emailSecondary = null,
            string phoneNumberPrimary = null,
            string phoneNumberSecondary = null,
            string notes = null)
        {
            Id = id;
            Username = username;
            DisplayName = displayName;
            Type = type;
            AdGroups = adGroups;
            TbServiceCodes = tbServiceCodes ?? new List<string>();
            IsReadOnly = isReadOnly;
            IsActive = isActive;
            JobTitle = jobTitle;
            EmailPrimary = emailPrimary;
            EmailSecondary = emailSecondary;
            PhoneNumberPrimary = phoneNumberPrimary;
            PhoneNumberSecondary = phoneNumberSecondary;
            Notes = notes;
        }

        public static IEnumerable<TestUser> GetAll() => new[]
        {
            ServiceUserForAbingdonAndPermitted,
            ServiceUserWithNoTbServices,
            RegionalUserWithPermittedPhecCode,
            UserWithServiceAndRegion,
            NationalTeamUser,
            AbingdonCaseManager,
            AbingdonCaseManager2,
            GatesheadCaseManager,
            GatesheadCaseManager2,
            InactiveGatesheadCaseManager,
            Developer,
            ReadOnlyUser,
            UntrustedContentUser
        };

        public static TestUser ServiceUserForAbingdonAndPermitted = new TestUser(
            1234,
            "abingdon@nhs.uk",
            "Abingdon Permitted",
            UserType.ServiceOrRegionalUser,
            new[] { Utilities.ABINGDON_AD_GROUP, Utilities.ASHFORD_AD_GROUP },
            tbServiceCodes: new[]
            {
                Utilities.TBSERVICE_ABINGDON_COMMUNITY_HOSPITAL_ID,
                Utilities.PERMITTED_SERVICE_CODE
            });

        public static TestUser ServiceUserWithNoTbServices = new TestUser(
            2345,
            "no-service@nhs.uk",
            "No Service",
            UserType.ServiceOrRegionalUser,
            new string[] { });

        public static TestUser RegionalUserWithPermittedPhecCode = new TestUser(
            3456,
            "permitted-phec@phe.com",
            "Permitted Phec",
            UserType.ServiceOrRegionalUser,
            new[] { Utilities.ADMIN_AD_GROUP, Utilities.SOUTH_EAST_AD_GROUP });

        public static TestUser UserWithServiceAndRegion = new TestUser(
            9988,
            "user-service-and-region@phe.com",
            "Service and Region",
            UserType.ServiceOrRegionalUser,
            new[] { Utilities.SOUTH_EAST_AD_GROUP, Utilities.ABINGDON_AD_GROUP });

        public static TestUser NationalTeamUser = new TestUser(
            4567,
            "national-team@ntbs.com",
            "National Team",
            UserType.NationalTeam,
            new[] { Utilities.ADMIN_AD_GROUP, Utilities.NATIONAL_AD_GROUP });

        public static TestUser AbingdonCaseManager = new TestUser(
            5678,
            Utilities.CASEMANAGER_ABINGDON_EMAIL,
            "TestCase TestManager",
            UserType.ServiceOrRegionalUser,
            new[] { Utilities.ABINGDON_AD_GROUP },
            tbServiceCodes: new[] { Utilities.TBSERVICE_ABINGDON_COMMUNITY_HOSPITAL_ID });

        public static TestUser AbingdonCaseManager2 = new TestUser(
            6789,
            Utilities.CASEMANAGER_ABINGDON_EMAIL2,
            "TestCase TestManager",
            UserType.ServiceOrRegionalUser,
            new[] { Utilities.ABINGDON_AD_GROUP },
            tbServiceCodes: new[] { Utilities.TBSERVICE_ABINGDON_COMMUNITY_HOSPITAL_ID });

        public static TestUser GatesheadCaseManager = new TestUser(
            Utilities.CASEMANAGER_GATESHEAD_ID1,
            Utilities.CASEMANAGER_GATESHEAD_EMAIL1,
            Utilities.CASEMANAGER_GATESHEAD_DISPLAY_NAME1,
            UserType.ServiceOrRegionalUser,
            new[] { Utilities.GATESHEAD_AD_GROUP },
            tbServiceCodes: new[] { Utilities.TBSERVICE_GATESHEAD_AND_SOUTH_TYNESIDE_ID });

        public static TestUser GatesheadCaseManager2 = new TestUser(
            Utilities.CASEMANAGER_GATESHEAD_ID2,
            Utilities.CASEMANAGER_GATESHEAD_EMAIL2,
            Utilities.CASEMANAGER_GATESHEAD_DISPLAY_NAME2,
            UserType.ServiceOrRegionalUser,
            new[] { Utilities.GATESHEAD_AD_GROUP },
            tbServiceCodes: new[] { Utilities.TBSERVICE_GATESHEAD_AND_SOUTH_TYNESIDE_ID });

        public static TestUser InactiveGatesheadCaseManager = new TestUser(
            Utilities.CASEMANAGER_GATESHEAD_INACTIVE_ID,
            Utilities.CASEMANAGER_GATESHEAD_INACTIVE_EMAIL,
            Utilities.CASEMANAGER_GATESHEAD_INACTIVE_DISPLAY_NAME,
            UserType.ServiceOrRegionalUser,
            new[] { Utilities.GATESHEAD_AD_GROUP },
            tbServiceCodes: new[] { Utilities.TBSERVICE_GATESHEAD_AND_SOUTH_TYNESIDE_ID },
            isActive: false);

        public static TestUser Developer = new TestUser(
            7890,
            "Developer@ntbs.phe.com",
            "BaseTestCase BaseTestManager",
            UserType.NationalTeam,
            new[] { Utilities.NATIONAL_AD_GROUP });

        public static TestUser ReadOnlyUser = new TestUser(
            7892,
            "ReadOnly@ntbs.phe.com",
            "ReadOnly UserGroup",
            UserType.ServiceOrRegionalUser,
            new[] { Utilities.READ_ONLY_AD_GROUP },
            isReadOnly: true);

        public static TestUser UntrustedContentUser = new TestUser
        (
            7893,
            "someone@ntbs.phe.com",
            "Someone",
            UserType.NationalTeam,
            new[] { Utilities.NATIONAL_AD_GROUP },
            jobTitle: "{abc}",
            emailPrimary: "{{def}}",
            emailSecondary: "g{{hi}}",
            phoneNumberPrimary: "j}{kl}",
            phoneNumberSecondary: "{{}}",
            notes: "}}mno"
        );
    }
}
