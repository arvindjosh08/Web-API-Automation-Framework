using Newtonsoft.Json;
using NLog;
using RestSharp;

namespace Api.Automation.Tests.Base
{
    public abstract class BaseService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        protected readonly RestClient _client;
        protected BaseService(RestClient client)
        {
            _client = client;
        }

        protected async Task<RestResponse> GetAsync(string resource, Dictionary<string, string> headers = null)
        {
            var request = new RestRequest(resource, Method.Get);

            AddHeaders(request, headers);

            Logger.Info($"Executing GET {resource}");
            var response = await _client.ExecuteAsync(request);
            Logger.Info($"Response: {response.StatusCode}, {response.Content}");
            return response;
        }

        protected async Task<RestResponse> PostJsonAsync<T>(string resource, T payload, Dictionary<string, string> headers = null) where T : class
        {
            var request = new RestRequest(resource, Method.Post);
            AddHeaders(request, headers);
            request.AddJsonBody(payload);

            Logger.Info($"Executing POST {resource} with JSON body");
            var response = await _client.ExecuteAsync(request);
            Logger.Info($"Response: {response.StatusCode}, {response.Content}");
            return response;
        }

        protected async Task<RestResponse> PostFormUrlEncodedAsync(string resource, Dictionary<string, string> formData, Dictionary<string, string> headers = null)
        {
            var request = new RestRequest(resource, Method.Post);
            AddHeaders(request, headers);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            foreach (var pair in formData)
            {
                request.AddParameter(pair.Key, pair.Value);
            }

            Logger.Info($"Executing POST {resource} with form data");
            var response = await _client.ExecuteAsync(request);
            Logger.Info($"Response: {response.StatusCode}, {response.Content}");
            return response;
        }
        private void AddHeaders(RestRequest request, Dictionary<string, string> headers)
        {
            if (headers == null) return;

            foreach (var header in headers)
            {
                request.AddHeader(header.Key, header.Value);
            }
        }
    }
}