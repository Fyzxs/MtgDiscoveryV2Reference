using Lib.UniversalCore.Serializes;
using Newtonsoft.Json;

namespace Web.MtgDiscovery.Databases.AzureSql.Models;

public sealed class CollectorSetAllCardsCountDetailsModel : ISelfSerialize
{
    [JsonProperty("unmodified")]
    public int Unmodified { get; set; }

    [JsonProperty("signed")]
    public int Signed { get; set; }

    [JsonProperty("altered")]
    public int ArtistAltered { get; set; }

    [JsonProperty("uniqueCount")]
    public int UniqueCount { get; set; }

    [JsonProperty("total")]
    public int Total => Unmodified + Signed + ArtistAltered;
}
