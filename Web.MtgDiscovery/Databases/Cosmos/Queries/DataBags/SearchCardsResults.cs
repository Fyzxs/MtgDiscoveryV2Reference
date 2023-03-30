using Newtonsoft.Json;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags;

public sealed class SearchCardsResults
{
    [JsonProperty("card_name")]
    public string Name { get; set; }

    [JsonProperty("card_count")]
    public int CardCount { get; set; }
}