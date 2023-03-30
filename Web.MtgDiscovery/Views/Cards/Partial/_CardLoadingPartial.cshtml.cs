using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.MtgDiscovery.Views.Cards.Partial
{
    public sealed class CardLoadingModel : PageModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CollectorNumber { get; set; }
        public string Rarity { get; set; }
        public int Total { get; set; }
        public string Price { get; set; }
        public string ReleasedAt { get; set; }

        public void OnGet()
        {
        }
    }
}
