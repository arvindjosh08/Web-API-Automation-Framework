using OpenQA.Selenium;
using Ui.Automation.Tests.Base;
using NLog;

namespace Ui.Automation.Tests.Pages
{
    public class ProductsPage
    {
        private readonly IWebDriver driver;
        private ElementActions actions;
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public ProductsPage(IWebDriver driver)
        {
            this.driver = driver;
            this.actions = new ElementActions(driver);
        }

        private readonly By searchedProducts = By.XPath("//div[contains(@class,'productinfo')]//p");
        private readonly By searchInputBox = By.Name("search");
        private readonly By searchButton = By.Id("submit_search");
        private readonly By searchedProductTitle = By.XPath(" //div[contains(@class,'features_items')]//h2[contains(@class,'title')]");

        public void EnterProductName(String productName)
        {
            actions.SendKeys(driver.FindElement(searchInputBox), productName, "Search Input Box");
        }
        public void ClickSearchButton()
        {
            actions.Click(driver.FindElement(searchButton), "Search Button");
        }

        public String GetSearchedProductTitle()
        {
            return actions.GetText(driver.FindElement(searchedProductTitle), "Searched Product Title");
        }

        public Boolean IsSearchedProductsRelevant(String expectedKeyword)
        {
            Boolean allRelevant = true;
            var productsList = driver.FindElements(searchedProducts);
            foreach (var product in productsList)
            {
                var productName = actions.GetText(product, "Searched Product Name");
                if (!productName.ToLower().Contains(expectedKeyword))
                {
                    allRelevant = false;
                    logger.Info($"Irrelevant product found: {productName}");
                }
            }
            return allRelevant;
        }
    }
}