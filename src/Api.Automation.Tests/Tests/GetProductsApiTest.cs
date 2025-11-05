using Api.Automation.Tests.Base;
using Api.Automation.Tests.Services;
using Api.Automation.Tests.Utilities;
using Newtonsoft.Json;
using Api.Automation.Tests.Models.ResponseModels;

namespace Api.Automation.Tests.Tests

{
    [TestClass]
    public class GetProductApiTests : BaseApiTest
    {
        private GetProducts _getProducts;

        [TestInitialize]
        public void SetupService()
        {
            // Each test gets its own service instance
            _getProducts = new GetProducts(client); }

        [TestMethod]
        [TestCategory("api")]
        public async Task GetProducts_ShouldReturn200()
        {
            logger.Info("*******STARTING -  GetProducts_ShouldReturn200 test");
            var response = await _getProducts.GetProductList();
            ProductResponseDto responseObj = JsonConvert.DeserializeObject<ProductResponseDto>(response.Content);
            Assert.AreEqual(200, responseObj.ResponseCode);
        }
        
        [TestMethod]
        [TestCategory("api")]
        public async Task GetProducts_SchemaValidation()
        {
            logger.Info("*******STARTING -  GetProducts_SchemaValidation test");
            var response = await _getProducts.GetProductList();
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(baseDirectory, "src/Api.Automation.Tests/Data/ApiSchema/GetProductsApiSchema.json");
            Assert.IsTrue(JsonSchemaValidator.IsSchemaValid(response.Content, fullPath));
        }

    }
}