using System;
using Lib.UniversalCore.Serializes;
using Newtonsoft.Json;

namespace Web.MtgDiscovery.Databases.AzureSql.Models;

public sealed class CollectorSetCountDetailsModel : ISelfSerialize
{
    [JsonProperty("id")]
    public Guid SetId { get; set; }

    [JsonProperty("counts")]
    public int Counts { get; set; }
}
