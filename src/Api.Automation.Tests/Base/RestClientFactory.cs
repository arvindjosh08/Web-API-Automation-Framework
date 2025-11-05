using RestSharp;

namespace Api.Automation.Tests.Base
{
    public static class RestClientFactory
    {
        public static RestClient GetClient(string baseUrl)
        {
            var options = new RestClientOptions(baseUrl)
            {
                ThrowOnAnyError = false,
                MaxTimeout = 10000
            };
            return new RestClient(options);
        }
    }
}