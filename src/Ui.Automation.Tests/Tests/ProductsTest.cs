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
            WebDriverFactory.GetDriver().Navigate().GoToUrl("https://www.automationexercise.com/");
            var homePage = new HomePage();
            homePage.ClickProducts();
            var productsPage = new ProductsPage();
            productsPage.EnterProductName("jeans");
            productsPage.ClickSearchButton();
            Assert.AreEqual("SEARCHED PRODUCTS", productsPage.GetSearchedProductTitle(), "Searched Products title does not match");
            Assert.IsTrue(productsPage.IsSearchedProductsRelevant("jeans"), "Some searched products are not relevant");
        }

    } 
}