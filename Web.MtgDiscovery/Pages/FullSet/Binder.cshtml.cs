using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.MtgDiscovery.UserStuff;

namespace Web.MtgDiscovery.Pages.FullSet
{
    public sealed class BinderModel : PageModel
    {
        public IActionResult OnGet(string collector, string code)
        {
            HasCollector = string.IsNullOrWhiteSpace(collector) is false;
            if (HasCollector) return Page();

            IAuthnDetails userDetails = new AuthnUserDetails(User);
            if (userDetails.IsNotAuthenticated()) return Page();
            if (userDetails.MissingId()) return Page();

            return RedirectToPage("BinderModel", new { collector = userDetails.CollectorId(), code });
        }
        public bool HasCollector { get; protected set; }
    }
}
