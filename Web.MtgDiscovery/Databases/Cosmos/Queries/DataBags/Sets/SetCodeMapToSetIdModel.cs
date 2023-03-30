using Newtonsoft.Json;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags.Sets;

public sealed class SetCodeMapToSetIdModel
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }
}
