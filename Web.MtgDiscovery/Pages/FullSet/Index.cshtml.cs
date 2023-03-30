using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.MtgDiscovery.UserStuff;

namespace Web.MtgDiscovery.Pages.FullSet
{
    public sealed class FullSetModel : PageModel
    {
        public IActionResult OnGet(string collector, string code)
        {
            HasCollector = string.IsNullOrWhiteSpace(collector) is false;
            SetCode = code;
            Collector = collector;

            IAuthnDetails userDetails = new AuthnUserDetails(User);
            DangerZoneAllowed = userDetails.CollectorId() == Collector;

            if (HasCollector) return Page();
            if (userDetails.IsNotAuthenticated()) return Page();
            if (userDetails.MissingId()) return Page();

            return RedirectToPage("Index", new { collector = userDetails.CollectorId(), code });
        }
        public bool HasCollector { get; protected set; }
        public string Collector { get; protected set; }
        public string SetCode { get; protected set; }

        public bool DangerZoneAllowed { get; protected set; }
    }
}
