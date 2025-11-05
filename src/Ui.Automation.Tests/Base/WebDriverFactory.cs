using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Ui.Automation.Tests.Base
{
    public static class WebDriverFactory
    {
        // Thread-safe WebDriver instance
        private static ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();

        public static IWebDriver GetDriver()
        {
            return driver.Value;
        }

        /// <summary>
        /// Initializes WebDriver based on the browser parameter.
        /// </summary>
        public static void SetDriver(string browser)
        {
            if (driver.Value == null)
            {
                IWebDriver driverInstance = CreateDriverInstance(browser);
                driver.Value = driverInstance;
            }
        }

        /// <summary>
        /// Quits the WebDriver for the current thread.
        /// </summary>
        public static void QuitDriver()
        {
            IWebDriver driverInstance = driver.Value;
            if (driverInstance != null)
            {
                driverInstance.Quit();
                driver.Value = null;
            }
        }

        /// <summary>
        /// Creates and configures WebDriver instance based on browser type.
        /// </summary>
        private static IWebDriver CreateDriverInstance(string browser)
        {
            IWebDriver driverInstance;

            switch (browser.ToLower())
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--start-maximized");
                    chromeOptions.AddArgument("--disable-notifications");
                    chromeOptions.AddArgument("--remote-allow-origins=*");
                    chromeOptions.AddArguments("--disable-popup-blocking");
                    chromeOptions.AddArguments("--disable-infobars");
                    chromeOptions.AddArguments("--disable-extensions");
                    chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.notifications", 2);
                    chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.popups", 0);
                    chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.automatic_downloads", 1);
                    chromeOptions.AddUserProfilePreference("profile.block_third_party_cookies", true);
                    chromeOptions.AddUserProfilePreference("profile.managed_default_content_settings.ads", 2);
                    chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.javascript", 1);
                    driverInstance = new ChromeDriver(chromeOptions);
                    break;

                case "edge":
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddArgument("--start-maximized");
                    driverInstance = new EdgeDriver(edgeOptions);
                    break;

                case "firefox":
                    var firefoxOptions = new FirefoxOptions();
                    driverInstance = new FirefoxDriver(firefoxOptions);
                    driverInstance.Manage().Window.Maximize();
                    break;

                default:
                    throw new ArgumentException($"Unsupported browser: {browser}");
            }

            return driverInstance;
        }
    }
}