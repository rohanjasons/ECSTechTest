using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Web.Test.Constants;
using Web.Test.Enums;
using System.Runtime.InteropServices;

namespace Web.Test.Selenium
{
    /// <summary>
    /// Base class used for all tests to inherit.
    /// </summary>
    public abstract class WebTestBase
    {
        /// <summary>
        /// Gets instance of the WebDriver used during the tests.
        /// </summary>
        public static IWebDriver WebBrowserDriver { get; set; }

        /// <summary>
        /// Start up the WebDriver and navigate to the URL specified.
        /// </summary>
        /// <param name="url">The Url that will be loaded in the web page.</param>
        /// <param name="deleteAllCookies">Should the cookies be deleted before starting the browser.</param>
        /// <param name="webDriver">The web driver that will be used during the test.</param>
        public static void CommonTestSetup(Uri url, bool deleteAllCookies, WebDriver webDriver, [Optional] string userName, [Optional] string accessKey)
        {
            switch (webDriver)
            {
                case WebDriver.Chrome:
                    InitialiseChromeLocal(url, deleteAllCookies);
                    break;
                case WebDriver.ChromeBuild:
                    InitialiseChromeBuild(url, deleteAllCookies);
                    break;
                case WebDriver.ChromeHeadless:
                    InitialiseChromeHeadless(url, deleteAllCookies);
                    break;
                case WebDriver.ChromeBrowserStack:
                    InitialiseChromeBrowserstack(url, deleteAllCookies, userName, accessKey);
                    break;
            }
        }

        /// <summary>
        /// Initialize chrome on the local web driver
        /// </summary>
        /// <param name="url">The url that will be navigated to</param>
        /// <param name="deleteAllCookies">Boolean to determine whether you want to delete cookies prior to opening browser</param>
        private static void InitialiseChromeLocal(Uri url, bool deleteAllCookies)
        {
            var options = new ChromeOptions();
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("download.directory_upgrade", true);
            options.AddUserProfilePreference("safebrowsing.enabled", true);
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddArguments("chrome.switches", "--disable-gpu", "--disable-popup-blocking", "--disable-extensions", "--disable-extensions-http-throttling", "--disable-extensions-file-access-check", "--disable-infobars", "--enable-automation", "--safebrowsing-disable-download-protection ", "--safebrowsing-disable-extension-blacklist", "--start-maximized");
            WebBrowserDriver = new ChromeDriver(options);
            InitialiseWebDriver(url, deleteAllCookies);
        }

        /// <summary>
        /// Initialize chrome on the local web driver
        /// </summary>
        /// <param name="url">The url that will be navigated to</param>
        /// <param name="deleteAllCookies">Boolean to determine whether you want to delete cookies prior to opening browser</param>
        private static void InitialiseChromeHeadless(Uri url, bool deleteAllCookies)
        {
            var options = new ChromeOptions();
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("download.directory_upgrade", true);
            options.AddUserProfilePreference("safebrowsing.enabled", true);
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddArguments("chrome.switches", "headless", "--disable-gpu", "--disable-popup-blocking", "--disable-extensions", "--disable-extensions-http-throttling", "--disable-extensions-file-access-check", "--disable-infobars", "--enable-automation", "--safebrowsing-disable-download-protection ", "--safebrowsing-disable-extension-blacklist", "--start-maximized");
            WebBrowserDriver = new ChromeDriver(options);
            InitialiseWebDriver(url, deleteAllCookies);
        }

        /// <summary>
        /// Initialize chrome on the build server
        /// </summary>
        /// <param name="url">The url that will be navigated to</param>
        /// <param name="deleteAllCookies">Boolean to determine whether you want to delete cookies prior to opening browser</param>
        private static void InitialiseChromeBuild(Uri url, bool deleteAllCookies)
        {
            var options = new ChromeOptions();
            options.AddUserProfilePreference("download.prompt_for_download", false);
            options.AddUserProfilePreference("download.directory_upgrade", true);
            options.AddUserProfilePreference("safebrowsing.enabled", true);
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddArguments("chrome.switches", "--disable-popup-blocking", "--disable-extensions", "--disable-extensions-http-throttling", "--disable-extensions-file-access-check", "--disable-infobars", "--enable-automation", "--safebrowsing-disable-download-protection ", "--safebrowsing-disable-extension-blacklist", "--start-maximized");
            options.AddArguments("headless");
            WebBrowserDriver = new ChromeDriver(Environment.GetEnvironmentVariable("ChromeWebDriver"), options);
            InitialiseWebDriver(url, deleteAllCookies);
        }

        /// <summary>
        /// Initialize the browserstack to run
        /// </summary>
        /// <param name="url"></param>
        /// <param name="deleteAllCookies"></param>
        private static void InitialiseChromeBrowserstack(Uri url, bool deleteAllCookies, string userName, string accessKey)
        {
            var options = new ChromeOptions();
            options.AddAdditionalCapability("os", "Windows", true);
            options.AddAdditionalCapability("os_version", "10", true);
            options.AddAdditionalCapability("browser", "Chrome", true);
            options.AddAdditionalCapability("browser_version", "latest", true);
            options.AddAdditionalCapability("browserstack.local", "false", true);
            options.AddAdditionalCapability("browserstack.selenium_version", "3.14.0", true);
            options.AddAdditionalCapability("browserstack.user", userName, true);
            options.AddAdditionalCapability("browserstack.key", accessKey, true);
            WebBrowserDriver = new RemoteWebDriver(new Uri("https://hub-cloud.browserstack.com/wd/hub"), options);
            InitialiseWebDriver(url, deleteAllCookies);
        }

        /// <summary>
        /// Navigate to inputted Url and maximize browser
        /// </summary>
        /// <param name="url">The url that will be navigated to</param>
        /// <param name="deleteAllCookies">Boolean to determine whether you want to delete cookies prior to opening browser</param>
        private static void InitialiseWebDriver(Uri url, bool deleteAllCookies)
        {
            const int maxAttempts = 3;
            for (var attempt = 1; attempt <= maxAttempts; attempt++)
            {
                var message = string.Empty;
                try
                {
                    SetTimeout.PageLoad(WebBrowserDriver, TimeSpan.FromSeconds(TimeoutInSeconds.DefaultTimeout));

                    if (deleteAllCookies)
                    {
                        WebBrowserDriver.Manage().Cookies.DeleteAllCookies();
                    }

                    WebBrowserDriver.Navigate().GoToUrl(url);
                    break;
                }
                catch (WebDriverException exception)
                {
                    message = message + $"Exception {attempt}:" + exception.Message;
                    if (attempt >= maxAttempts)
                    {
                        throw new WebDriverException(string.Format($"Failed to start Web Browser in timely manner. - {message}"));
                    }
                }
            }
        }
    }
}