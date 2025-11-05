using NLog;
using RestSharp;
using Api.Automation.Tests.Utilities;

namespace Api.Automation.Tests.Base
{
    public abstract class BaseApiTest
    {
        protected static readonly Logger logger = LogManager.GetCurrentClassLogger();


        // Thread-safe service instance for the test
        protected RestClient client;

        [TestInitialize]
        public void TestSetup()
        {
            // Initialize RestClient per test for thread-safety
            client = RestClientFactory.GetClient(ConfigReader.Get("BaseUrl"));

            logger.Info($"Test '{TestContext.TestName}' started on Thread ID: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            logger.Info($"Test '{TestContext.TestName}' finished on Thread ID: {System.Threading.Thread.CurrentThread.ManagedThreadId}");
        }
        public TestContext TestContext { get; set; }
    }
}