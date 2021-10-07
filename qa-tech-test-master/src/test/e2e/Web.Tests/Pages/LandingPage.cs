using System;
using OpenQA.Selenium;
using Web.Integration.Test.Helper;
using Web.Integration.Test.Pages.Base;
using Web.Test.Extensions;

namespace Web.Integration.Test.Pages
{
    public class LandingPage : BaseWebPage
    {
        private readonly IWebDriver _driver;

        public LandingPage(IWebDriver webDriver) : base(webDriver)
        {
            _driver = webDriver;
        }

        private IWebElement ChallengeButton()
        {
            return _driver.FindElement(By.XPath("//*[contains(@data-test-id,'render-challenge')]"));
        }

        private IWebElement DataTable()
        {
            return _driver.FindElement(By.XPath("//*[@id=\"challenge\"]/div/div/div[1]/div/div[2]/table"));
        }

        private IWebElement TableRows(int row)
        {
            return _driver.FindElement(By.XPath($"//*[@id=\"challenge\"]/div/div/div[1]/div/div[2]/table/tbody/tr[{row}]"));
        }

        private IWebElement ChallengeInputOne()
        {
            return _driver.FindElement(By.XPath("//*[contains(@data-test-id,'submit-1')]"));
        }

        private IWebElement ChallengeInputTwo()
        {
            return _driver.FindElement(By.XPath("//*[contains(@data-test-id,'submit-2')]"));
        }

        private IWebElement ChallengeInputThree()
        {
            return _driver.FindElement(By.XPath("//*[contains(@data-test-id,'submit-3')]"));
        }

        private IWebElement ChallengeInputName()
        {
            return _driver.FindElement(By.XPath("//*[contains(@data-test-id,'submit-4')]"));
        }

        private IWebElement SubmitChallengeButton()
        {
            return _driver.FindElement(By.XPath("//*[contains(text(),'Submit Answers')]"));
        }

        private IWebElement SuccessBanner()
        {
            return _driver.FindElement(By.XPath("//*[contains(text(),'Congratulations you have succeeded. Please submit your challenge ✅')]"));
        }

        private IWebElement FailureBanner()
        {
            return _driver.FindElement(By.XPath("//*[contains(text(),'It looks like your answer wasn')]")); 
        }

        private IWebElement CloseSuccessBannerButton()
        {
            return _driver.FindElement(By.XPath("//*[contains(text(),'Close')]"));
        }

        public LandingPage StartChallenge()
        {
            ChallengeButton().ClickElement(_driver);
            return this;
        }

        public LandingPage FindIndex(string name)
        {
            EnterResults(name);
            return this;
        }

        public LandingPage SubmitResults()
        {
            SubmitChallengeButton().Click(_driver);
            return this;
        }

        public LandingPage CloseBanner()
        {
            CloseSuccessBannerButton().Click(_driver);
            return this;
        }

        private Tuple<int, int, int> ConvertTableFindIndex()
        {
            var index1 = 0;
            var index2 = 0;
            var index3 = 0;

            DataTable().WaitForElementIsVisible();
            var rows = TestHelper.GetRowCount(DataTable());
            for(int i = 0; i < rows; i++)
            {
                switch(i)
                {
                    case 0:
                        var row1 = TestHelper.ArrayConvertor(TableRows(i+1));
                        index1 = TestHelper.CheckPointArraySumMatches(row1);
                        break;
                    case 1:
                        var row2 = TestHelper.ArrayConvertor(TableRows(i+1));
                        index2 = TestHelper.CheckPointArraySumMatches(row2);
                        break;
                    case 2:
                        var row3 = TestHelper.ArrayConvertor(TableRows(i+1));
                        index3 = TestHelper.CheckPointArraySumMatches(row3);
                        break;
                }
            }
            return Tuple.Create(index1, index2, index3);
        }

        public bool IsTableDisplayed()
        {
            return DataTable().WaitForElementIsVisible();
        }

        public bool IsBannerDisplayed(string success)
        {
            var result = false;
            switch(success)
            {
                case "success":
                    if (SuccessBanner().WaitForElementIsVisible())
                        result = true;
                    break;
                case "failure":
                    if (FailureBanner().WaitForElementIsVisible())
                        result = true;
                    break;
            }
            return result;

        }

        private void EnterResults(string name)
        {
            var results = ConvertTableFindIndex();
            ChallengeInputOne().EnterText(_driver, results.Item1.ToString());
            ChallengeInputTwo().EnterText(_driver, results.Item2.ToString());
            ChallengeInputThree().EnterText(_driver, results.Item3.ToString());
            ChallengeInputName().EnterText(_driver, name);
        }
    }
}