using System;
using Lib.UniversalCore.Serializes;
using Newtonsoft.Json;

namespace Web.MtgDiscovery.Databases.AzureSql.Models;

public sealed class CollectorSetCardCountDetailsModel : ISelfSerialize
{
    [JsonProperty("id")]
    public Guid CardId { get; set; }

    [JsonProperty("counts")]
    public int Counts { get; set; }
}