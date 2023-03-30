using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.MtgDiscovery.UserStuff;

namespace Web.MtgDiscovery.Pages.AllSets
{
    public sealed class AllSetsModel : PageModel
    {
        public IActionResult OnGet(string collector)
        {
            HasCollector = string.IsNullOrWhiteSpace(collector) is false;
            if (HasCollector) return Page();

            IAuthnDetails userDetails = new AuthnUserDetails(User);
            if (userDetails.IsNotAuthenticated()) return Page();
            if (userDetails.MissingId()) return Page();

            return RedirectToPage("Index", new { collector = userDetails.CollectorId() });
        }

        public bool HasCollector { get; protected set; }
    }
}
