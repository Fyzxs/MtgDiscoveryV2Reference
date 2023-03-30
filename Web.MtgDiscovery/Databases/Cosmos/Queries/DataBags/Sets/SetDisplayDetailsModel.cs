﻿using Lib.UniversalCore.Serializes;
using Newtonsoft.Json;

namespace Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags.Sets;

public sealed class SetDisplayDetailsModel : ISelfSerialize
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("scryfall_id")]
    public string ScryFallId { get; set; }

    [JsonProperty("filter_attributes")]
    public string[] FilterAttribute { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("original_code")]
    public string OriginalCode { get; set; }

    [JsonProperty("base_set_size")]
    public string BaseSetSize { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("parent_set_code")]
    public string ParentSetCode { get; set; }

    [JsonProperty("released_at")]
    public string ReleasedAt { get; set; }

    [JsonProperty("set_type")]
    public string SetType { get; set; }
}
