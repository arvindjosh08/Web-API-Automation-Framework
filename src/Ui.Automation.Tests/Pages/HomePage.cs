using OpenQA.Selenium;
using Ui.Automation.Tests.Base;
using NLog;

namespace Ui.Automation.Tests.Pages
{
    public class HomePage
    {
        private ElementActions actions;
        private readonly IWebDriver driver;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            this.actions = new ElementActions(driver);
        }

        private readonly By navigationBar = By.XPath("//ul[contains(@class,'navbar')]//a");
        private readonly By loggedInUser = By.XPath("//ul[contains(@class,'navbar')]//a[contains(text(),'Logged')]");

        public void ClickLogin()
        {

            var navigationList = driver.FindElements(navigationBar);
            foreach (var navItem in navigationList)
            {
                var loginInnerText = actions.GetText(navItem, "Login/Signup navigation item");
                if (loginInnerText.Trim().Contains("Signup / Login", StringComparison.OrdinalIgnoreCase))
                {
                    actions.Click(navItem, "Login Navigation Item");
                    break;
                }
            }
        }

        public String GetLoggedInUserName()
        {
            return actions.GetText(loggedInUser, "Logged in user name element", 10);
        }

        public void ClickProducts()
        {
            var navigationList = driver.FindElements(navigationBar);
            foreach (var navItem in navigationList)
            {
                var productsInnerText = actions.GetText(navItem, "Products navigation item");
                if (productsInnerText.Trim().Contains("Products", StringComparison.OrdinalIgnoreCase))
                {
                    actions.Click(navItem, "Products Navigation Item");
                    break;
                }
            }
        }

        public void ClickDeleteAccount()
        {
            var navigationList = WebDriverFactory.GetDriver().FindElements(navigationBar);
            foreach (var navItem in navigationList)
            {
                var deleteInnerText = actions.GetText(navItem, "Delete Account navigation item");
                if (deleteInnerText.Trim().Contains("Delete", StringComparison.OrdinalIgnoreCase))
                {
                    actions.Click(navItem, "Delete Account Navigation Item");
                    break;
                }
            }
        }
    }
}