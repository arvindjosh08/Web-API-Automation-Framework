using Ui.Automation.Tests.Base;
using Ui.Automation.Tests.Pages;

namespace Ui.Automation.Tests.Tests
{

    [TestClass]
    public class SignUpTest : BaseTest
    {

        [TestMethod]
        [TestCategory("ui")]
        public  void VerifySignUpAndDeleteFunctionality()
        {
            logger.Info("*******STARTING -  VerifySignUpAndDeleteFunctionality test");
            driver.Navigate().GoToUrl("https://www.automationexercise.com/");
            HomePage homePage = new HomePage(driver);
            homePage.ClickLogin();
            LoginPage loginPage = new LoginPage(driver);
           var randomName= loginPage.EnterSignUpName();
            loginPage.EnterSignUpEmail();
            loginPage.ClickSignUpButton();
            SignUpPage signUpPage = new SignUpPage(driver);
            signUpPage.EnterPassword();
            signUpPage.EnterFirstName();
            signUpPage.EnterLastName();
            signUpPage.EnterAddress();
            signUpPage.EnterState();
            signUpPage.EnterCity();
            signUpPage.EnterZipCode();
            signUpPage.EnterMobile();
            signUpPage.ClickSubmit();        
            Assert.AreEqual("ACCOUNT CREATED!",signUpPage.GetAccountCreateText());
            signUpPage.ClickContinueButton();
            Assert.AreEqual("Logged in as" + " " + randomName, homePage.GetLoggedInUserName(), "Logged in user name does not match");
            homePage.ClickDeleteAccount();
            Assert.AreEqual("ACCOUNT DELETED!", signUpPage.GetAccountDeletionText(), "Account deletion text does not match");
        }
    } 
}