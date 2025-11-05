using Newtonsoft.Json;

namespace Api.Automation.Tests.Models.ResponseModels
{

public class CategoryResDto
{
    [JsonProperty("usertype")]
    public UserTypeResDto UserType { get; set; }

    [JsonProperty("category")]
    public string CategoryName { get; set; }
}
}