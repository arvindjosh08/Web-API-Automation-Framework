using Newtonsoft.Json;

namespace Api.Automation.Tests.Models.ResponseModels
{
    public class ProductResponseDto
    {
        [JsonProperty("responseCode")]
        public int ResponseCode { get; set; }

        [JsonProperty("products")]
        public List<ProductResDto> Products { get; set; }
    }
}