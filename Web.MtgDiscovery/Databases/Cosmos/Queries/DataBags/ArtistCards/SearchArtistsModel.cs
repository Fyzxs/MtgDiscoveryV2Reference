using Lib.UniversalCore.Serializes;
using Newtonsoft.Json;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags.ArtistCards;

public sealed class SearchArtistsModel : ISelfSerialize
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("names")]
    public string[] Names { get; set; }

    [JsonProperty("card_count")]
    public int CardCount { get; set; }
}
