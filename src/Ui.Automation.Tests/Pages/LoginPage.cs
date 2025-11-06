using OpenQA.Selenium;
using Ui.Automation.Tests.Base;
using NLog;

namespace Ui.Automation.Tests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        private ElementActions actions;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            this.actions = new ElementActions(driver); // ElementActions fetches driver internally
        }

        private readonly By emailAddress = By.Name("email");
        private readonly By password = By.Name("password");
        private readonly By loginButton = By.CssSelector("button[data-qa='login-button']");
        private readonly By signUpName = By.XPath("//input[@name='name']");
        private readonly By signUpEmail = By.XPath("//input[@data-qa='signup-email']");
        
        private readonly By signUpButton = By.CssSelector("button[data-qa='signup-button']");


        public void EnterEmailAddress()
        {
            actions.SendKeys(driver.FindElement(emailAddress), "janison@gmail.com", "Email Address Input");
        }

        public void EnterPassword()
        {
            actions.SendKeys(driver.FindElement(password), "testing", "Email Address Input");
        }

        public void ClickLogin()
        {
            actions.Click(driver.FindElement(loginButton), "Login Button");
        }

        public void EnterSignUpEmail()
        {
            string randomEmail = "User_" + Guid.NewGuid().ToString("N") + "@example.com";
            actions.SendKeys(driver.FindElement(signUpEmail), randomEmail, "Email Address Input");
        }

        public string EnterSignUpName()
        {
            string randomName = "Product_" + Guid.NewGuid().ToString("N");
            actions.SendKeys(driver.FindElement(signUpName), randomName, "SignUp Name Input");
            return randomName;
        }

        public void ClickSignUpButton()
        {
            actions.Click(driver.FindElement(signUpButton), "SignUp Button");
        }
    }
}