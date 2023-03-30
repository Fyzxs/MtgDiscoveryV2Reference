using Newtonsoft.Json;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags;

public interface ICardLoading
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("collector_number")]
    public string CollectorNumber { get; set; }

    [JsonProperty("rarity")]
    public string Rarity { get; set; }

    [JsonProperty("current_price")]
    string Price { get; set; }

    [JsonProperty("released_at")]
    public string ReleasedAt { get; set; }
}
