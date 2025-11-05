using OpenQA.Selenium;
using Ui.Automation.Tests.Base;
using NLog;

namespace Ui.Automation.Tests.Pages
{
    public class LoginPage : BasePage
    {
        private ElementActions actions;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public LoginPage() : base()
        {

            this.actions = new ElementActions(); // ElementActions fetches driver internally
        }

        private readonly By emailAddress = By.Name("email");
        private readonly By password = By.Name("password");
        private readonly By loginButton = By.CssSelector("button[data-qa='login-button']");
        private readonly By signUpName = By.XPath("//input[@name='name']");
        private readonly By signUpEmail = By.XPath("//input[@data-qa='signup-email']");
        
        private readonly By signUpButton = By.CssSelector("button[data-qa='signup-button']");


        public void EnterEmailAddress()
        {
            actions.SendKeys(WebDriverFactory.GetDriver().FindElement(emailAddress), "janison@gmail.com", "Email Address Input");
        }

        public void EnterPassword()
        {
            actions.SendKeys(WebDriverFactory.GetDriver().FindElement(password), "testing", "Email Address Input");
        }

        public void ClickLogin()
        {
            actions.Click(WebDriverFactory.GetDriver().FindElement(loginButton), "Login Button");
        }

        public void EnterSignUpEmail()
        {
            string randomEmail = "User_" + Guid.NewGuid().ToString("N") + "@example.com";
            actions.SendKeys(WebDriverFactory.GetDriver().FindElement(signUpEmail), randomEmail, "Email Address Input");
        }

        public string EnterSignUpName()
        {
            string randomName = "Product_" + Guid.NewGuid().ToString("N");
            actions.SendKeys(WebDriverFactory.GetDriver().FindElement(signUpName), randomName, "SignUp Name Input");
            return randomName;
        }

        public void ClickSignUpButton()
        {
            actions.Click(WebDriverFactory.GetDriver().FindElement(signUpButton), "SignUp Button");
        }
    }
}