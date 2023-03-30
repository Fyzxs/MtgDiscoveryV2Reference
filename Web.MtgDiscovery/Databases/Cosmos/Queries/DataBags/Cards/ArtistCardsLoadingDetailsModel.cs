using Lib.UniversalCore.Serializes;
using Newtonsoft.Json;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags.Cards;

public sealed class ArtistCardsLoadingDetailsModel : ISelfSerialize
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("discovery_set_id")]
    public string DiscoverySetId { get; set; }

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

    [JsonProperty("code")]
    public string Code { get; set; }
}
