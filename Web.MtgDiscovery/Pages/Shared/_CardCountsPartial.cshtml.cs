using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Web.MtgDiscovery.Pages.Shared
{
    public sealed class CardCountsModel : PageModel
    {
        [JsonProperty("unmodified")]
        public int Owned { get; set; }

        [JsonProperty("signed")]
        public int Signed { get; set; }

        [JsonProperty("artistProof")]
        public int ArtistProof { get; set; }

        [JsonProperty("altered")]
        public int ArtistAltered { get; set; }

        public bool HasCollector { get; set; }

        public void OnGet()
        {
        }
    }
}
