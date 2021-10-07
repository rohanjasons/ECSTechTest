using System;
using BoDi;
using Helper.Test.Configuration;
using Shouldly;
using TechTalk.SpecFlow;
using Web.Integration.Test.Pages;
using Web.Integration.Test.Pages.Base;
using Web.Test.Enums;
using Web.Tests.Tests.Base;

namespace Web.Tests.Tests.specFlow.StepDefinition
{
    [Binding]
    public class EcsFeatureStepDefinition : WebBase
    {
        protected EcsFeatureStepDefinition(ConfigurationFixture testConfig, IObjectContainer objectContainer)
        {
            this.testConfig = testConfig;
            this.objectContainer = objectContainer;
        }

        private string Url = "http://localhost:3000";

        [Given("I have started the technical challenge")]
        public void IHaveStartedTheTechnicalChallenge()
        {
            CommonTestSetup(new Uri(Url), true, WebDriver.Chrome); // "testConfig.BaseUrl" in order to achieve tokenisation and reinforce no secrets in code I would call from the test config JSON file. In order to get this to work the user may need to update the preference to copy the file to output which would fail the brief of the tests running without interference.
            var ChallengeStarted = WebBrowserDriver
                .LandingPage()
                .StartChallenge();
            ChallengeStarted.IsTableDisplayed().ShouldBeTrue();

            objectContainer.RegisterInstanceAs(ChallengeStarted, "ChallengeStarted");
        }

        [When(@"I complete the array challenge")]
        public void WhenICompleteTheArrayChallenge()
        {
            var AnswersInputted = objectContainer.Resolve<LandingPage>("ChallengeStarted")
                  .FindIndex("Rohan Jasons");

            objectContainer.RegisterInstanceAs(AnswersInputted, "AnswersInputted");
        }

        [When(@"I submit the '(.*)' challenge results")]
        public void WhenISubmitTheChallengeResults(string completionStatus)
        {
            switch (completionStatus)
            {
                case "completed":
                    var AnswersSubmitted = objectContainer.Resolve<LandingPage>("AnswersInputted")
                        .SubmitResults();
                    objectContainer.RegisterInstanceAs(AnswersSubmitted, "AnswersSubmitted");
                    break;
                case "incomplete":
                    var EmptyAnswers = objectContainer.Resolve<LandingPage>("ChallengeStarted")
                        .SubmitResults();
                    objectContainer.RegisterInstanceAs(EmptyAnswers, "AnswersSubmitted");
                    break;
            }
        }

        [Then(@"a '(.*)' banner is displayed")]
        public void ThenABannerIsDisplayed(string success)
        {
            var result = objectContainer.Resolve<LandingPage>("AnswersSubmitted");

            switch (success)
            {
                case "success":
                    result.IsBannerDisplayed(success).ShouldBeTrue();
                    result.CloseBanner();
                    break;
                case "failure":
                    result.IsBannerDisplayed(success).ShouldBeTrue();
                    result.CloseBanner();
                    break;
            }
        }
    }
}