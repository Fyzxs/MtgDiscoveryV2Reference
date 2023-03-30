using System.Globalization;
using Lib.UniversalCore.Configurations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Web.MtgDiscovery.Pages.Shared;

namespace Web.MtgDiscovery.Views.Cards.Partial
{
    public sealed class CardDisplayModel : PageModel
    {
        private static readonly IConfig s_config = new MonoStateConfig();

        [JsonIgnore]
        public CardCountsModel CountsModel { get; set; } = new CardCountsModel();

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("discovery_set_id")]
        public string DiscoverySetId { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("rune_code")]
        public string RuneCode { get; set; }

        [JsonProperty("set_name")]
        public string SetName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("collector_number")]
        public string CollectorNumber { get; set; }

        [JsonProperty("rarity")]
        public string Rarity { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("scryfall_uri")]
        public string ScryFallUri { get; set; }

        [JsonProperty("released_at")]
        public string ReleasedAt { get; set; }

        [JsonProperty("nonfoil")]
        public bool NonFoil { get; set; }

        [JsonProperty("foil")]
        public bool Foil { get; set; }

        [JsonProperty("has_reverse")]
        public bool HasReverse { get; set; }

        [JsonProperty("etched")]
        public bool Etched { get; set; }

        [JsonProperty("textured")]
        public bool Textured { get; set; }

        [JsonProperty("current_price")]
        public string Price { get; set; }

        [JsonIgnore]
        public string FormattedPrice => double.TryParse(Price, out double result) ? $"{result.ToString("C", CultureInfo.GetCultureInfo("en-US"))}" : "$0.00";

        [JsonIgnore]
        public int AlteredCount { get; set; }

        [JsonIgnore]
        public int ArtistProofCount { get; set; }

        [JsonIgnore]
        public int SignedCount { get; set; }

        [JsonIgnore]
        public int UnmodifiedCount { get; set; }

        [JsonIgnore]
        public int TotalCount => AlteredCount + SignedCount + UnmodifiedCount;

        [JsonIgnore]
        public string Collector { get; set; } = "";

        [JsonIgnore]
        public bool IsUrlCollector { get; set; }
        public string ImageUrl => s_config["Urls:Images"];

        public void OnGet()
        {
        }
    }
}
