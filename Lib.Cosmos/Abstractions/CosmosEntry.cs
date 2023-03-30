using System;
using Newtonsoft.Json;

namespace Lib.Cosmos.Abstractions
{
    public /*Cosmos Required*/ class CosmosEntry
    {
        [JsonProperty("body")]
        public dynamic Body { get; set; }

        [JsonProperty("id")]
        public virtual Guid Id { get; set; }
    }
}
