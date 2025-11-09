using Ui.Automation.Tests.Base;
using NLog;
using OpenQA.Selenium;

namespace Ui.Automation.Tests.Base
{
    [TestClass]
    public class BaseTest
    {
        protected IWebDriver driver;

        public TestContext TestContext { get; set; }
        protected static readonly Logger logger = LogManager.GetCurrentClassLogger();


        [TestInitialize]
        public void SetUp()
        {
            try
            {
                string browser = GetBrowserName();
                WebDriverFactory.SetDriver(browser);
                driver = WebDriverFactory.GetDriver();
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to initialize WebDriver: {e.Message}", e);
            }
        }


        private string GetBrowserName()
        {
            string browser = null;

            if (TestContext != null && ((System.Collections.IDictionary)TestContext.Properties).Contains("Browser"))
            {
                browser = TestContext.Properties["Browser"]?.ToString();
            }

            if (string.IsNullOrEmpty(browser))
            {
                browser = Environment.GetEnvironmentVariable("BROWSER");
            }

            if (string.IsNullOrEmpty(browser))
            {
                browser = "chrome";
            }

            Console.WriteLine($"[INFO] Browser set to: {browser}");
            return browser.ToLower();
        }

        /// <summary>
        /// Cleans up WebDriver after each test.
        /// </summary>
        [TestCleanup]
        public void TearDown()
        {
            try
            {
                if (WebDriverFactory.GetDriver() != null)
                {
                    WebDriverFactory.QuitDriver();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while quitting driver: {e.Message}");
            }
        }
    }
}