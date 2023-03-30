using System;
using Lib.UniversalCore.Serializes;
using Newtonsoft.Json;

namespace Web.MtgDiscovery.Databases.AzureSql.Models;

public sealed class CollectorCardCountDetailsModel : ISelfSerialize
{
    [JsonProperty("id")]
    public Guid CardId { get; set; }

    [JsonProperty("unmodified")]
    public int Unmodified { get; set; }

    [JsonProperty("signed")]
    public int Signed { get; set; }

    [JsonProperty("altered")]
    public int ArtistAltered { get; set; }

    [JsonProperty("artistProof")]
    public int ArtistProof { get; set; }
}