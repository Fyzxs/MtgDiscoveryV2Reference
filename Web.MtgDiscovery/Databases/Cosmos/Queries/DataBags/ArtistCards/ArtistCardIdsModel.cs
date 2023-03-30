using Newtonsoft.Json;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags.ArtistCards;

public sealed class ArtistCardIdsModel
{
    [JsonProperty("cards")]
    public string[] Cards { get; set; }
}
