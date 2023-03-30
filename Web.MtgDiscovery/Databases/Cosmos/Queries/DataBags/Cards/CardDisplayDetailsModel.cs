using Lib.UniversalCore.Serializes;
using Newtonsoft.Json;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags.Cards;

public sealed class CardDisplayDetailsModel : ISelfSerialize
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("discovery_set_id")]
    public string DiscoverySetId { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("rune_code")]
    public string RuneCode { get; set; }

    [JsonProperty("set_name")]
    public string SetName { get; set; }

    [JsonProperty("foil")]
    public bool Foil { get; set; }

    [JsonProperty("nonfoil")]
    public bool NonFoil { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("collector_number")]
    public string CollectorNumber { get; set; }

    [JsonProperty("rarity")]
    public string Rarity { get; set; }

    [JsonProperty("artist")]
    public string Artist { get; set; }

    [JsonProperty("scryfall_uri")]
    public string ScryFallUri { get; set; }

    [JsonProperty("released_at")]
    public string ReleasedAt { get; set; }

    [JsonProperty("current_price")]
    string Price { get; set; }

    [JsonProperty("has_reverse")]
    public bool HasReverse { get; set; }

    [JsonProperty("etched")]
    public bool Etched { get; set; }

    [JsonProperty("textured")]
    public bool Textured { get; set; }
}
