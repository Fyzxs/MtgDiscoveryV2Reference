using Newtonsoft.Json;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags;

public interface ICollectorSet
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("unmodified")]
    public int Unmodified { get; set; }

    [JsonProperty("signed")]
    public int Signed { get; set; }

    [JsonProperty("artistProof")]
    public int ArtistProof { get; set; }

    [JsonProperty("altered")]
    public int ArtistAltered { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("uniqueCount")]
    public int UniqueCount { get; set; }
}
