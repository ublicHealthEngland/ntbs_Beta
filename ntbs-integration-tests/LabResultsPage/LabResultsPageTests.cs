﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ntbs_integration_tests.Helpers;
using ntbs_integration_tests.TestServices;
using ntbs_service;
using ntbs_service.Models.Entities;
using ntbs_service.Models.Enums;
using ntbs_service.Services;
using Xunit;
using IndexModel = ntbs_service.Pages.LabResults.IndexModel;

namespace ntbs_integration_tests.LabResultsPage
{
    public class LabResultsPageTests : TestRunnerBase
    {
        public LabResultsPageTests(NtbsWebApplicationFactory<Startup> factory) : base(factory) { }

        public static IList<Notification> GetSeedingNotifications()
        {
            return new List<Notification>
            {
                new Notification
                {
                    NotificationId = Utilities.SPECIMEN_MATCHING_NOTIFICATION_ID1,
                    NotificationStatus = NotificationStatus.Notified
                },
                new Notification
                {
                    NotificationId = Utilities.SPECIMEN_MATCHING_NOTIFICATION_ID2,
                    NotificationStatus = NotificationStatus.Notified
                },
                new Notification
                {
                    NotificationId = Utilities.SPECIMEN_MATCHING_NOTIFICATION_ID3,
                    NotificationStatus = NotificationStatus.Notified
                },
                new Notification
                {
                    NotificationId = Utilities.SPECIMEN_MATCHING_NOTIFICATION_ID4,
                    NotificationStatus = NotificationStatus.Notified
                },
                new Notification
                {
                    NotificationId = Utilities.SPECIMEN_MATCHING_MANUAL_MATCH_NOTIFICATION_ID,
                    NotificationStatus = NotificationStatus.Notified
                }
            };
        }

        [Fact]
        public async Task NhsUser_CanViewSpecimensAccordingToPermissions()
        {
            using (var client = Factory.WithMockUserService<TestNhsUserService>()
                .CreateClientWithoutRedirects())
            {
                // Arrange
                var expectedLabReferenceNumbers = new List<string>
                {
                    MockSpecimenService.MockUnmatchedSpecimenForTbService.ReferenceLaboratoryNumber
                };
                var notExpectedLabReferenceNumbers = new List<string>
                {
                    MockSpecimenService.MockUnmatchedSpecimenForPhec.ReferenceLaboratoryNumber
                };

                //Act
                var response = await client.GetAsync("/LabResults");
                var document = await GetDocumentAsync(response);

                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var specimenDetailsSections = document.QuerySelectorAll(".specimen-details");
                Assert.Equal(expectedLabReferenceNumbers.Count, specimenDetailsSections.Length);

                foreach (var expectedLabReferenceNumber in expectedLabReferenceNumbers)
                {
                    var header = document.QuerySelector($"#specimen-{expectedLabReferenceNumber}");
                    Assert.NotNull(header);
                }

                foreach (var notExpectedLabReferenceNumber in notExpectedLabReferenceNumbers)
                {
                    var header = document.QuerySelector($"#specimen-{notExpectedLabReferenceNumber}");
                    Assert.Null(header);
                }
            }
        }

        [Fact]
        public async Task NhsUser_ShowsNoSpecimensIfNoPermissionForTbServices()
        {
            using (var client = Factory.WithMockUserService<TestWithoutTbServicesNhsUserService>()
                .CreateClientWithoutRedirects())
            {
                //Act
                var response = await client.GetAsync("/LabResults");
                var document = await GetDocumentAsync(response);

                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var specimenDetailsSections = document.QuerySelectorAll(".specimen-details");
                Assert.Equal(0, specimenDetailsSections.Length);
            }
        }

        [Fact]
        public async Task PheUser_CanViewSpecimensAccordingToPermissions()
        {
            using (var client = Factory.WithMockUserService<TestPheUserService>()
                .CreateClientWithoutRedirects())
            {
                // Arrange
                var expectedLabReferenceNumbers = new List<string>
                {
                    MockSpecimenService.MockUnmatchedSpecimenForPhec.ReferenceLaboratoryNumber
                };
                var notExpectedLabReferenceNumbers = new List<string>
                {
                    MockSpecimenService.MockUnmatchedSpecimenForTbService.ReferenceLaboratoryNumber
                };

                //Act
                var response = await client.GetAsync("/LabResults");
                var document = await GetDocumentAsync(response);

                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var specimenDetailsSections = document.QuerySelectorAll(".specimen-details");
                Assert.Equal(expectedLabReferenceNumbers.Count, specimenDetailsSections.Length);

                foreach (var expectedLabReferenceNumber in expectedLabReferenceNumbers)
                {
                    var header = document.QuerySelector($"#specimen-{expectedLabReferenceNumber}");
                    Assert.NotNull(header);
                }

                foreach (var notExpectedLabReferenceNumber in notExpectedLabReferenceNumbers)
                {
                    var header = document.QuerySelector($"#specimen-{notExpectedLabReferenceNumber}");
                    Assert.Null(header);
                }
            }
        }

        [Fact]
        public async Task NationalTeam_CanViewSpecimensAccordingToPermissions()
        {
            using (var client = Factory.WithMockUserService<TestNationalTeamUserService>()
                .CreateClientWithoutRedirects())
            {
                // Arrange
                var expectedLabReferenceNumbers = new List<string>
                {
                    MockSpecimenService.MockUnmatchedSpecimenForTbService.ReferenceLaboratoryNumber,
                    MockSpecimenService.MockUnmatchedSpecimenForPhec.ReferenceLaboratoryNumber
                };

                //Act
                var response = await client.GetAsync("/LabResults");
                var document = await GetDocumentAsync(response);

                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                var specimenDetailsSections = document.QuerySelectorAll(".specimen-details");
                Assert.Equal(expectedLabReferenceNumbers.Count, specimenDetailsSections.Length);

                foreach (var expectedLabReferenceNumber in expectedLabReferenceNumbers)
                {
                    var header = document.QuerySelector($"#specimen-{expectedLabReferenceNumber}");
                    Assert.NotNull(header);
                }
            }
        }

        [Fact]
        public async Task NationalTeam_CanMatchSpecimenFromCandidatePotentialMatch()
        {
            using (var client = Factory.WithMockUserService<TestNationalTeamUserService>()
                .CreateClientWithoutRedirects())
            {
                // Arrange
                var expectedUnmatchedSpecimen =
                    MockSpecimenService.MockUnmatchedSpecimenForTbService;
                var specimenNumber = expectedUnmatchedSpecimen.ReferenceLaboratoryNumber;
                var candidateMatchNotificationId = expectedUnmatchedSpecimen.PotentialMatches.First().NotificationId;

                const string url = "/LabResults/";
                var response = await client.GetAsync(url);
                var document = await GetDocumentAsync(response);

                var formData = new Dictionary<string, string>
                {
                    [$"PotentialMatchSelections[{specimenNumber}].NotificationId"] =
                        candidateMatchNotificationId.ToString(),
                    [$"PotentialMatchSelections[{specimenNumber}].ManualNotificationId"] = ""
                };

                // Act
                var postResponse = await client.SendPostFormWithData(document, formData, url);

                // Assert
                await AssertAndFollowRedirect(postResponse, url);

                // As session/tempData aren't functional by default with webApplicationFactory, and configuring this
                // wasn't deemed a good use of time, cannot confirm that the flash message is shown here.
                // Additionally as we're using a mocked specimen service, the unmatched specimen is not removed from the
                // rendered values.
            }
        }
        
        [Fact]
        public async Task NationalTeam_CanMatchSpecimenForManualNotificationId()
        {
            using (var client = Factory.WithMockUserService<TestNationalTeamUserService>()
                .CreateClientWithoutRedirects())
            {
                // Arrange
                var expectedUnmatchedSpecimen =
                    MockSpecimenService.MockUnmatchedSpecimenForTbService;
                var specimenNumber = expectedUnmatchedSpecimen.ReferenceLaboratoryNumber;
                const int manualMatchNotificationId = Utilities.SPECIMEN_MATCHING_MANUAL_MATCH_NOTIFICATION_ID;

                const string url = "/LabResults/";
                var response = await client.GetAsync(url);
                var document = await GetDocumentAsync(response);

                var formData = new Dictionary<string, string>
                {
                    [$"PotentialMatchSelections[{specimenNumber}].NotificationId"] = 
                        IndexModel.ManualNotificationIdValue.ToString(),
                    [$"PotentialMatchSelections[{specimenNumber}].ManualNotificationId"] =
                        manualMatchNotificationId.ToString(),
                };

                // Act
                var postResponse = await client.SendPostFormWithData(document, formData, url);

                // Assert
                await AssertAndFollowRedirect(postResponse, url);
                
                // As session/tempData aren't functional by default with webApplicationFactory, and configuring this
                // wasn't deemed a good use of time, cannot confirm that the flash message is shown here.
                // Additionally as we're using a mocked specimen service, the unmatched specimen is not removed from the
                // rendered values.
            }
        }
        
        [Fact]
        public async Task NationalTeam_CanNotManuallyMatchToNonExistentNotificationId_ValidationError()
        {
            using (var client = Factory.WithMockUserService<TestNationalTeamUserService>()
                .CreateClientWithoutRedirects())
            {
                // Arrange
                var expectedUnmatchedSpecimen =
                    MockSpecimenService.MockUnmatchedSpecimenForTbService;
                var specimenNumber = expectedUnmatchedSpecimen.ReferenceLaboratoryNumber;
                const int manualMatchNotificationId = 999145;

                const string url = "/LabResults/";
                var response = await client.GetAsync(url);
                var document = await GetDocumentAsync(response);

                var formData = new Dictionary<string, string>
                {
                    [$"PotentialMatchSelections[{specimenNumber}].NotificationId"] = 
                        IndexModel.ManualNotificationIdValue.ToString(),
                    [$"PotentialMatchSelections[{specimenNumber}].ManualNotificationId"] =
                        manualMatchNotificationId.ToString(),
                };

                // Act
                var result = await client.SendPostFormWithData(document, formData, url);
                var resultDocument = await GetDocumentAsync(result);

                // Assert
                result.AssertValidationErrorResponse();
                
                resultDocument.AssertErrorSummaryMessage(
                    $"PotentialMatchSelections[{specimenNumber}]-ManualNotificationId", 
                    $"PotentialMatchSelections[{specimenNumber}]-ManualNotificationId", 
                    "The notification ID does not exist, verify you have entered the correct ID before moving forward");

            }
        }
    }
}
