// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.0.0.0
//      SpecFlow Generator Version:3.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace ntbs_ui_tests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class NotificationCreationFeature : Xunit.IClassFixture<NotificationCreationFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "CreateNotifications.feature"
#line hidden
        
        public NotificationCreationFeature(NotificationCreationFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Notification creation", "    Happy and error paths for notification creation", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 4
    #line 5
        testRunner.Given("I am on the Search page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 6
        testRunner.When("I enter 1 into \'SearchParameters_IdFilter\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 7
        testRunner.And("I click on \'search-button\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 8
        testRunner.Then("I should be on the Search page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 9
        testRunner.When("I click on \'create-button\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 10
        testRunner.Then("I should be on the Patient page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute(DisplayName="Create and submit notification without errors")]
        [Xunit.TraitAttribute("FeatureTitle", "Notification creation")]
        [Xunit.TraitAttribute("Description", "Create and submit notification without errors")]
        public virtual void CreateAndSubmitNotificationWithoutErrors()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create and submit notification without errors", null, ((string[])(null)));
#line 12
    this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 4
    this.FeatureBackground();
#line 14
        testRunner.When("I enter Test into \'Patient_GivenName\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 15
        testRunner.And("I enter User into \'Patient_FamilyName\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
        testRunner.And("I enter 1 into \'FormattedDob_Day\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
        testRunner.And("I enter 1 into \'FormattedDob_Month\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
        testRunner.And("I enter 1970 into \'FormattedDob_Year\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
        testRunner.And("I select radio value \'sexId-1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
        testRunner.And("I select 1 for \'Patient_EthnicityId\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
        testRunner.And("I select radio value \'nhs-number-unknown\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
        testRunner.And("I select 1 for \'Patient_CountryId\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
        testRunner.And("I select radio value \'postcode-no\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
        testRunner.And("I click on \'save-button\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 27
        testRunner.Then("I should be on the Episode page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 28
        testRunner.When("I enter 1 into \'FormattedNotificationDate_Day\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 29
        testRunner.And("I enter 1 into \'FormattedNotificationDate_Month\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
        testRunner.And("I enter 2019 into \'FormattedNotificationDate_Year\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
        testRunner.And("I select TBS0008 for \'Episode_TBServiceCode\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
        testRunner.And("I wait for 1 second", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
        testRunner.And("I select 868e426f-b11d-45a3-bf2c-e0c31bed2c44 for \'Episode_HospitalId\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
        testRunner.And("I click on \'save-button\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
        testRunner.Then("I should be on the ClinicalDetails page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 39
        testRunner.When("I check \'NotificationSiteMap_PULMONARY_\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 40
        testRunner.When("I enter 1 into \'FormattedDiagnosisDate_Day\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 41
        testRunner.And("I enter 1 into \'FormattedDiagnosisDate_Month\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
        testRunner.And("I enter 2018 into \'FormattedDiagnosisDate_Year\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
        testRunner.And("I click on \'save-button\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
        testRunner.Then("I should be on the ContactTracing page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 46
        testRunner.When("I click on \'submit-button\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 47
        testRunner.Then("I should see the Notification", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Create and submit notification without content")]
        [Xunit.TraitAttribute("FeatureTitle", "Notification creation")]
        [Xunit.TraitAttribute("Description", "Create and submit notification without content")]
        public virtual void CreateAndSubmitNotificationWithoutContent()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create and submit notification without content", null, ((string[])(null)));
#line 49
    this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 4
    this.FeatureBackground();
#line 50
        testRunner.When("I click on \'submit-button\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 51
        testRunner.Then("I should be on the Patient page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 52
        testRunner.And("I should see all submission error messages", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Create and delete notification draft")]
        [Xunit.TraitAttribute("FeatureTitle", "Notification creation")]
        [Xunit.TraitAttribute("Description", "Create and delete notification draft")]
        public virtual void CreateAndDeleteNotificationDraft()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create and delete notification draft", null, ((string[])(null)));
#line 54
    this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 4
    this.FeatureBackground();
#line 55
        testRunner.When("I click on \'delete-draft-button\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 56
        testRunner.Then("I should be on the Delete page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 57
        testRunner.When("I click on \'confirm-deletion-button\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 58
        testRunner.Then("I should be on the Confirm page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 59
        testRunner.When("I click on \'return-to-homepage\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 60
        testRunner.Then("I should be on the Homepage", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Xunit.FactAttribute(DisplayName="Denotify a notification")]
        [Xunit.TraitAttribute("FeatureTitle", "Notification creation")]
        [Xunit.TraitAttribute("Description", "Denotify a notification")]
        public virtual void DenotifyANotification()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Denotify a notification", null, ((string[])(null)));
#line 63
    this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 4
    this.FeatureBackground();
#line 70
        testRunner.When("I enter Test into \'Patient_GivenName\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 71
        testRunner.And("I enter User into \'Patient_FamilyName\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 72
        testRunner.And("I enter 1 into \'FormattedDob_Day\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 73
        testRunner.And("I enter 1 into \'FormattedDob_Month\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 74
        testRunner.And("I enter 1970 into \'FormattedDob_Year\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 75
        testRunner.And("I select radio value \'sexId-1\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 76
        testRunner.And("I select 1 for \'Patient_EthnicityId\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 77
        testRunner.And("I select radio value \'nhs-number-unknown\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 78
        testRunner.And("I select 1 for \'Patient_CountryId\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 79
        testRunner.And("I select radio value \'postcode-no\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 80
        testRunner.And("I click on \'save-button\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 82
        testRunner.Then("I should be on the Episode page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 83
        testRunner.When("I enter 1 into \'FormattedNotificationDate_Day\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 84
        testRunner.And("I enter 1 into \'FormattedNotificationDate_Month\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 85
        testRunner.And("I enter 2019 into \'FormattedNotificationDate_Year\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 86
        testRunner.And("I select TBS0008 for \'Episode_TBServiceCode\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 87
        testRunner.And("I wait for 1 second", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 88
        testRunner.And("I select 868e426f-b11d-45a3-bf2c-e0c31bed2c44 for \'Episode_HospitalId\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 89
        testRunner.And("I click on \'save-button\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 91
        testRunner.Then("I should be on the ClinicalDetails page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 92
        testRunner.When("I check \'NotificationSiteMap_PULMONARY_\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 93
        testRunner.When("I enter 1 into \'FormattedDiagnosisDate_Day\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 94
        testRunner.And("I enter 1 into \'FormattedDiagnosisDate_Month\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 95
        testRunner.And("I enter 2018 into \'FormattedDiagnosisDate_Year\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 96
        testRunner.And("I click on \'save-button\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 98
        testRunner.Then("I should be on the ContactTracing page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 99
        testRunner.When("I click on \'submit-button\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 100
        testRunner.Then("I should see the Notification", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 103
        testRunner.Given("I am on current notification page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 104
        testRunner.When("I expand manage notification section", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 105
        testRunner.And("I click on \'denotify-button\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 106
        testRunner.Then("I should be on the Denotify page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 107
        testRunner.When("I select radio value \'denotify-radio-DuplicateEntry\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 108
        testRunner.And("I click on \'confirm-dentoification-button\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 109
        testRunner.Then("I should see the Notification", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 110
        testRunner.And("The notification should be denotified", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                NotificationCreationFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                NotificationCreationFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
