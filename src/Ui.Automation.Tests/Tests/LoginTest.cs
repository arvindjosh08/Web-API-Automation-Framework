using Ui.Automation.Tests.Base;
using Ui.Automation.Tests.Pages;

namespace Ui.Automation.Tests.Tests
{

    [TestClass]
    public class LoginTests : BaseTest
    {

        [TestMethod]
        [TestCategory("ui")]
        public void VerifyLoginFunctionality()
        {
            logger.Info("*******STARTING - VerifyLoginFunctionality test");
            WebDriverFactory.GetDriver().Navigate().GoToUrl("https://www.automationexercise.com/");
            var homePage = new HomePage();
            homePage.ClickLogin();
            var loginPage = new LoginPage();
            loginPage.EnterEmailAddress();
            loginPage.EnterPassword();
            loginPage.ClickLogin();
            Assert.AreEqual("Logged in as testing", homePage.GetLoggedInUserName(), "Logged in user name does not match");
        }
    } 
}