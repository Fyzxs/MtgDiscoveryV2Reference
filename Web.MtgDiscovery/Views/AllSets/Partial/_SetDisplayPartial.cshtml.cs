using System.Collections.Generic;
using Lib.UniversalCore.Configurations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Web.MtgDiscovery.Views.AllSets.Partial
{
    public sealed class SetDisplayModel : PageModel
    {
    }

    public sealed class SetDisplayCacheModel
    {
        private static readonly IConfig s_config = new MonoStateConfig();

        [JsonProperty("filter_attributes")]
        public List<string> FilterAttributes { get; set; }

        [JsonProperty("original_code")]
        public string OriginalCode { get; set; }

        [JsonProperty("base_set_size")]
        public int BaseSetSize { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parent_set_code")]
        public string ParentSetCode { get; set; }

        [JsonProperty("released_at")]
        public string ReleasedAt { get; set; }

        [JsonProperty("set_type")]
        public string SetType { get; set; }

        [JsonProperty("set_collection_count")]
        public int SetCollectionCount { get; set; }

        [JsonProperty("set_cards_count")]
        public int SetCardsCount { get; set; }

        [JsonIgnore]
        public string Collector { get; set; }

        [JsonIgnore]
        public int SetPercent => (SetCollectionCount * 100) / BaseSetSize;

        public string ImageUrl => s_config["Urls:Images"];
    }
}
