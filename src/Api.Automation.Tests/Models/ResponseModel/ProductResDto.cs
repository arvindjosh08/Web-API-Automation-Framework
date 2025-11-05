using Newtonsoft.Json;

namespace Api.Automation.Tests.Models.ResponseModels
{
    public class ProductResDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("category")]
        public CategoryResDto Category { get; set; }
    }
}