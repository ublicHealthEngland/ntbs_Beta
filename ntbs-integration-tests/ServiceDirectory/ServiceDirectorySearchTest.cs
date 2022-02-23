﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using ntbs_integration_tests.Helpers;
using ntbs_service;
using Xunit;

namespace ntbs_integration_tests.ServiceDirectory
{
    public class ServiceDirectorySearchTest : TestRunnerBase
    {
        public ServiceDirectorySearchTest(NtbsWebApplicationFactory<Startup> factory) : base(factory) { }

        public const string PageRoute = "ServiceDirectory";

        [Fact]
        public async Task GetSearch_ReturnsPageWithModelErrors_IfSearchNotValid()
        {
            // Arrange
            var initialPage = await Client.GetAsync(PageRoute);
            var pageContent = await GetDocumentAsync(initialPage);

            var formData = new Dictionary<string, string> {["SearchKeyword"] = "!!Test User#",};

            // Act
            var result = await SendGetFormWithData(pageContent, formData, PageRoute);

            // Assert
            var resultDocument = await GetDocumentAsync(result);
            resultDocument.AssertErrorMessage("search-keyword",
                "Search keyword can only contain letters and the symbols ' - . ,");
        }

        [Fact]
        public async Task GetSearch_RedirectToSearchResults_IfSearchValid()
        {
            // Arrange
            var initialPage = await Client.GetAsync(PageRoute);
            var pageContent = await GetDocumentAsync(initialPage);
            var formData = new Dictionary<string, string> {["SearchKeyword"] = "TestUser"};

            // Act
            var result = await SendGetFormWithData(pageContent, formData, PageRoute);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, result.StatusCode);
        }

        [Fact]
        public async Task HospitalSearch_DisplaysActiveHospitalsCorrectly()
        {
            // Arrange
            var initialPage = await Client.GetAsync(PageRoute);
            var pageContent = await GetDocumentAsync(initialPage);
            var formData = new Dictionary<string, string> {["SearchKeyword"] = "HoSpiTaL"};

            // Act
            var result = await SendGetFormWithData(pageContent, formData, PageRoute);
            var redirectLocation = GetRedirectLocation(result);
            var redirectPage = await Client.GetAsync(redirectLocation);
            var redirectPageContent = await GetDocumentAsync(redirectPage);

            var hospitalResults = redirectPageContent.GetElementsByClassName("directory-hospital-search-result-item");

            var queenElizabethSection = hospitalResults.Single(
                elem => elem.TextContent.Contains(Utilities.HOSPITAL_QUEEN_ELIZABETH_HOSPITAL_GATESHEAD_NAME)
            );

            // Assert
            Assert.Contains(hospitalResults,
                e => e.TextContent.Contains(Utilities.HOSPITAL_ABINGDON_COMMUNITY_HOSPITAL_NAME));
            Assert.Contains(hospitalResults,
                e => e.TextContent.Contains(Utilities.HOSPITAL_QUEEN_ELIZABETH_HOSPITAL_GATESHEAD_NAME));
            Assert.Contains(hospitalResults,
                e => e.TextContent.Contains(Utilities.HOSPITAL_SOUTH_TYNESIDE_DISTRICT_HOSPITAL_NAME));

            // Check service and region are populated for hospital
            Assert.Contains(Utilities.TBSERVICE_GATESHEAD_AND_SOUTH_TYNESIDE_NAME, queenElizabethSection.TextContent);
            Assert.Contains(Utilities.NORTH_EAST_REGION_NAME, queenElizabethSection.TextContent);

            // Legacy hospital so should not show in search
            Assert.DoesNotContain(hospitalResults,
                e => e.TextContent.Contains(Utilities.HOSPITAL_FULWOOD_HALL_HOSPITAL_NAME));
        }

        [Fact]
        public async Task TBServiceSearch_DisplaysActiveServicesCorrectly()
        {
            // Arrange
            var initialPage = await Client.GetAsync(PageRoute);
            var pageContent = await GetDocumentAsync(initialPage);
            var formData = new Dictionary<string, string> {["SearchKeyword"] = "Gateshead and South Tyneside"};

            // Act
            var result = await SendGetFormWithData(pageContent, formData, PageRoute);
            var redirectLocation = GetRedirectLocation(result);
            var redirectPage = await Client.GetAsync(redirectLocation);
            var redirectPageContent = await GetDocumentAsync(redirectPage);

            var serviceResults = redirectPageContent.GetElementsByClassName("directory-service-search-result-item");

            // Assert
            Assert.Contains(serviceResults,
                e => e.TextContent.Contains(Utilities.TBSERVICE_GATESHEAD_AND_SOUTH_TYNESIDE_NAME));
            // Check Region is displayed for tb service
            Assert.Contains(Utilities.NORTH_EAST_REGION_NAME, serviceResults.Single().TextContent);
            // Legacy service so should not show in search
            Assert.DoesNotContain(serviceResults,
                e => e.TextContent.Contains(Utilities.TBSERVICE_GATESHEAD_AND_SOUTH_TYNESIDE_LEGACY_NAME));
        }

        [Fact]
        public async Task RegionSearch_DisplaysRegionsCorrectly()
        {
            // Arrange
            var initialPage = await Client.GetAsync(PageRoute);
            var pageContent = await GetDocumentAsync(initialPage);
            var formData = new Dictionary<string, string> {["SearchKeyword"] = "East"};

            // Act
            var result = await SendGetFormWithData(pageContent, formData, PageRoute);
            var redirectLocation = GetRedirectLocation(result);
            var redirectPage = await Client.GetAsync(redirectLocation);
            var redirectPageContent = await GetDocumentAsync(redirectPage);

            var regionResults = redirectPageContent.GetElementsByClassName("directory-region-search-result-item");

            // Assert
            Assert.Contains(regionResults, e => e.TextContent.Contains(Utilities.NORTH_EAST_REGION_NAME));
            Assert.Contains(regionResults, e => e.TextContent.Contains(Utilities.SOUTH_EAST_REGION_NAME));
            Assert.Contains(regionResults, e => e.TextContent.Contains(Utilities.EAST_OF_ENGLAND_REGION_NAME));
            Assert.Contains(regionResults, e => e.TextContent.Contains(Utilities.EAST_MIDLANDS_REGION_NAME));
            Assert.DoesNotContain(regionResults, e => e.TextContent.Contains(Utilities.LONDON_REGION_NAME));
        }

        [Fact]
        public async Task Pagination_IsWorkingCorrectly()
        {
            // Arrange First Page
            var searchKeyword = "a";
            var pageOffset = 20;
            var formData = new Dictionary<string, string> {["SearchKeyword"] = searchKeyword};

            var initialPage = await Client.GetAsync(PageRoute);
            var pageContent = await GetDocumentAsync(initialPage);

            // Act First Page
            var result = await SendGetFormWithData(pageContent, formData, PageRoute);
            var redirectLocation = GetRedirectLocation(result);
            var firstPage = await Client.GetAsync(redirectLocation);
            var firstPageContent = await GetDocumentAsync(firstPage);

            var totalNumberOfResults = GetTotalNumberOfResults(firstPageContent);
            var firstPageDisplayedResults = GetNumberOfDisplayedItems(firstPageContent);
            var (firstPageRegionResults, 
                firstPageServiceResults, 
                firstPageHospitalResults, 
                firstPageUserResults) =
                    GetSearchResultsTextContents(firstPageContent);
            
            // Assert First Page
            Assert.Equal(20, firstPageDisplayedResults);
            Assert.Equal(28, totalNumberOfResults);

            // Arrange second page
            var secondPage = await Client.GetAsync(PageRoute + $"/SearchResults?SearchKeyword={searchKeyword}&pageIndex=2&offset={pageOffset}");
            var secondPageContent = await GetDocumentAsync(secondPage);

            // Act second page
            var secondPageNumberOfResults = GetTotalNumberOfResults(secondPageContent);
            var secondPageDisplayedResults = GetNumberOfDisplayedItems(secondPageContent);
            var (secondPageRegionResults, 
                secondPageServiceResults, 
                secondPageHospitalResults, 
                secondPageUserResults) =
                    GetSearchResultsTextContents(secondPageContent);

            // Assert second page
            Assert.Equal(28, secondPageNumberOfResults);
            Assert.Equal(totalNumberOfResults - pageOffset, secondPageDisplayedResults);

            // Check no results from first page are shown on second page
            Assert.DoesNotContain(secondPageRegionResults, sprr => firstPageRegionResults.Any(fprr => fprr == sprr));
            Assert.DoesNotContain(secondPageServiceResults, spsr => firstPageServiceResults.Any(fpsr => fpsr == spsr));
            Assert.DoesNotContain(secondPageHospitalResults, sphr => firstPageHospitalResults.Any(fphr => fphr == sphr));
            Assert.DoesNotContain(secondPageUserResults, spur => firstPageUserResults.Any(fpur => fpur == spur));
        }

        private static int GetTotalNumberOfResults(IHtmlDocument firstPageContent)
        {
            return Convert.ToInt32(
                firstPageContent.GetElementsByClassName("case-manager-results-summary")
                    .Single()
                    .TextContent
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)[1]
            );
        }

        private static int GetNumberOfDisplayedItems(IHtmlDocument pageContent)
        {
            var regionResults = pageContent.GetElementsByClassName("directory-region-search-result-item");
            var serviceResults = pageContent.GetElementsByClassName("directory-service-search-result-item");
            var hospitalResults = pageContent.GetElementsByClassName("directory-hospital-search-result-item");
            var userResults = pageContent.GetElementsByClassName("directory-user-search-result-item");
            return regionResults.Length + serviceResults.Length + hospitalResults.Length + userResults.Length;
        }

        private static (
            IEnumerable<string> secondPageRegionResults,
            IEnumerable<string> secondPageServiceResults,
            IEnumerable<string> secondPageHospitalResults,
            IEnumerable<string> secondPageUserResults)
            GetSearchResultsTextContents(
                IHtmlDocument secondPageContent)
        {
            var secondPageRegionResults = secondPageContent.GetElementsByClassName("directory-region-search-result-item").Select(e => e.TextContent);
            var secondPageServiceResults = secondPageContent.GetElementsByClassName("directory-service-search-result-item").Select(e => e.TextContent);
            var secondPageHospitalResults = secondPageContent.GetElementsByClassName("directory-hospital-search-result-item").Select(e => e.TextContent);
            var secondPageUserResults = secondPageContent.GetElementsByClassName("directory-user-search-result-item").Select(e => e.TextContent);
            return (secondPageRegionResults, secondPageServiceResults, secondPageHospitalResults,
                secondPageUserResults);
        }
    }
}
