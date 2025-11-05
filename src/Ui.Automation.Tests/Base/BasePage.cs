using OpenQA.Selenium;

namespace Ui.Automation.Tests.Base
{
    public abstract class BasePage
    {
        protected IWebDriver driver;

        public BasePage()
        {
            driver = WebDriverFactory.GetDriver();
        }
    }
}