using Ui.Automation.Tests.Base;
using Ui.Automation.Tests.Pages;

namespace Ui.Automation.Tests.Tests
{

    [TestClass]
    public class ProductsTest : BaseTest
    {

        [TestMethod]
        [TestCategory("ui")]
        public void VerifySearchProductsFunctionality()
        {
            logger.Info("*******STARTING -  VerifySearchProductsFunctionality test");
            driver.Navigate().GoToUrl("https://www.automationexercise.com/");
            HomePage homePage = new HomePage(driver);
            homePage.ClickProducts();
            ProductsPage productsPage = new ProductsPage(driver);
            productsPage.EnterProductName("jeans");
            productsPage.ClickSearchButton();
            Assert.AreEqual("SEARCHED PRODUCTS", productsPage.GetSearchedProductTitle(), "Searched Products title does not match");
            Assert.IsTrue(productsPage.IsSearchedProductsRelevant("jeans"), "Some searched products are not relevant");
        }

    } 
}