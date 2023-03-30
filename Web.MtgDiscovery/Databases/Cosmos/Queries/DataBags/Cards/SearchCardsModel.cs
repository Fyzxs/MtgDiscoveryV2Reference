using Lib.UniversalCore.Serializes;
using Newtonsoft.Json;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags.Cards;

public sealed class SearchCardsModel : ISelfSerialize
{
    [JsonProperty("card_name")]
    public string CardName { get; set; }

    [JsonProperty("card_count")]
    public int CardCount { get; set; }
}
