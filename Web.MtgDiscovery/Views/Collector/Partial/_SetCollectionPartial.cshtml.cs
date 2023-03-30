using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Web.MtgDiscovery.Views.Collector.Partial
{
    public sealed class SetCollectionPageModel : PageModel
    {
        public void OnGet()
        {
        }

    }

    public sealed class SetCollectionCacheModel
    {

        [JsonProperty("base_set_size")]
        public int BaseSetSize { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string SetName { get; set; }

        [JsonProperty("set_collection_count")]
        public int SetCollectionCount { get; set; }

        [JsonProperty("set_cards_count")]
        public int SetCardsCount { get; set; }

        [JsonIgnore]
        public int SetPercent => (SetCollectionCount * 100) / BaseSetSize;
    }
}
