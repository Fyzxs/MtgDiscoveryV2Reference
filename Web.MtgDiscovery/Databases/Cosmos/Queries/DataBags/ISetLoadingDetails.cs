using Newtonsoft.Json;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags;

public interface ISetLoadingDetails
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("original_code")]
    public string OriginalCode { get; set; }

    [JsonProperty("parent_set_code")]
    public string ParentSetCode { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("released_at")]
    public string ReleasedAt { get; set; }

    [JsonProperty("base_set_size")]
    public string BaseSetSize { get; set; }
}
