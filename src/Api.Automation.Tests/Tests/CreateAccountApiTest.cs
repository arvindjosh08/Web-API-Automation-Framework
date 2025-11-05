using Api.Automation.Tests.Base;
using Api.Automation.Tests.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Api.Automation.Tests.Models.ResponseModel;

namespace Api.Automation.Tests.Tests

{
    [TestClass]
    public class CreateAccountApiTest : BaseApiTest
    {
        private CreateAccount _createAccount;

        [TestInitialize]
        public void SetupService()
        {
            // Each test gets its own service instance
            _createAccount = new CreateAccount(client);
        }

        [TestMethod]
        [TestCategory("api")]
        public async Task CreateProduct_ShouldReturn201()
        {
            // 2. Read JSON from file
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(baseDirectory, "src/Api.Automation.Tests/Data/ApiRequests/CreateAccountReq.json");
            string json = File.ReadAllText(fullPath);
            var product = JsonConvert.DeserializeObject<CreateAccountReqDto>(json);

            // 3. Modify fields dynamically
            product.name = "Product_" + Guid.NewGuid().ToString("N");
            product.email = "User_" + Guid.NewGuid().ToString("N") + "@example.com";
            // 4. Convert to dictionary
            var dict = JObject.FromObject(product)
                              .ToObject<Dictionary<string, string>>();

            var response = await _createAccount.RegisterUser(dict);
            var responseObj = JsonConvert.DeserializeObject<CreateAccountResDto>(response.Content);
            Assert.AreEqual(201, responseObj.responseCode);
            Assert.AreEqual("User created!", responseObj.message);
            
        }

    }
}