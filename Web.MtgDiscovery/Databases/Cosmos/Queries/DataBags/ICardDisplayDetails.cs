using Newtonsoft.Json;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags;

public interface ICardDisplayDetails
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("set_name")]
    public string SetName { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("collector_number")]
    public string CollectorNumber { get; set; }

    [JsonProperty("rarity")]
    public string Rarity { get; set; }

    [JsonProperty("artist")]
    public string Artist { get; set; }

    [JsonProperty("nonfoil")]
    public bool NonFoil { get; set; }

    [JsonProperty("foil")]
    public bool Foil { get; set; }

    [JsonProperty("scryfall_uri")]
    public string ScryFallUri { get; set; }

    [JsonProperty("released_at")]
    public string ReleasedAt { get; set; }

    [JsonProperty("has_reverse")]
    public bool HasReverse { get; set; }

    [JsonProperty("current_price")]
    string Price { get; set; }
}
