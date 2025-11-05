using OpenQA.Selenium;
using Ui.Automation.Tests.Base;
using NLog;
using OpenQA.Selenium.Support.UI;

namespace Ui.Automation.Tests.Pages
{
    public class SignUpPage : BasePage
    {
        private ElementActions actions;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public SignUpPage() : base()
        {
            this.actions = new ElementActions(); // ElementActions fetches driver internally
        }


        private readonly By passwordInputBox = By.Id("password");
        private readonly By firstNameInputBox = By.Id("first_name");
        private readonly By lastNameInputBox = By.Id("last_name");
        private readonly By address1InputBox = By.Id("address1");
        private readonly By stateInputBox = By.Id("state");
        private readonly By cityInputBox = By.Id("city");
        private readonly By zipCodeInputBox = By.Id("zipcode");
        private readonly By mobileNumInputBox = By.Id("mobile_number");
        private readonly By createAccountButton = By.CssSelector("button[data-qa='create-account']");
        private readonly By accountCreateText = By.XPath("//section[@id='form']//h2[contains(@data-qa,'account')]");
        private readonly By continueButton = By.XPath("//section[@id='form']//a[contains(@data-qa,'continue')]");
        private readonly By accountDeletion = By.XPath("//section[@id='form']//h2[contains(@data-qa,'account')]");



        public void EnterPassword()
        {
            actions.SendKeys(WebDriverFactory.GetDriver().FindElement(passwordInputBox), "abcdef", "Password Input Box", 10);
        }
        public void EnterFirstName()
        {
            actions.SendKeys(WebDriverFactory.GetDriver().FindElement(firstNameInputBox), "abcdef", "Fisrtname Input Box");
        }
        public void EnterLastName()
        {
            actions.SendKeys(WebDriverFactory.GetDriver().FindElement(lastNameInputBox), "abcdef", "Lastname Input Box");
        }
        public void EnterAddress()
        {
            actions.SendKeys(WebDriverFactory.GetDriver().FindElement(address1InputBox), "abcdef", "Address Input Box");
        }
        public void EnterState()
        {
            actions.SendKeys(WebDriverFactory.GetDriver().FindElement(stateInputBox), "NSW", "State Input Box");
        }
        public void EnterCity()
        {
            actions.SendKeys(WebDriverFactory.GetDriver().FindElement(cityInputBox), "Sydney", "City Input Box");
        }
        public void EnterZipCode()
        {
            actions.SendKeys(WebDriverFactory.GetDriver().FindElement(zipCodeInputBox), "2153", "Zipcode Input Box");
        }
        public void EnterMobile()
        {
            actions.SendKeys(WebDriverFactory.GetDriver().FindElement(mobileNumInputBox), "0404584545", "Mobile Number Input Box");
        }

        public void ClickSubmit()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(arguments[0], arguments[1]);", 0, 1200);
            actions.Click(WebDriverFactory.GetDriver().FindElement(createAccountButton), "Submit Button");
            WaitForContinueButtonToVisible();
        }

        public string GetAccountCreateText()
        {
            return actions.GetText(WebDriverFactory.GetDriver().FindElement(accountCreateText), "Account Created Text", 10);
        }

        public void ClickContinueButton()
        {
            actions.Click(WebDriverFactory.GetDriver().FindElement(continueButton), "Continue button");
        }

        public string GetAccountDeletionText()
        {
            return actions.GetText(WebDriverFactory.GetDriver().FindElement(accountDeletion), "Account Deletion Text");
        }

        public void ScrollToEnd()
        {
            actions.ScrollToEndOfPage();
        }
        public void WaitForContinueButtonToVisible()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.XPath("//section[@id='form']//a[contains(@data-qa,'continue')]")).Text.Contains("Continue"));
        }
    }
}