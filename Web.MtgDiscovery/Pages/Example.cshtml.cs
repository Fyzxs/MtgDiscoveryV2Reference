using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.MtgDiscovery.Pages
{
    public sealed class ExampleModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToPage("AllSets/Index", new { collector = "4e6a7ca7-7be2-4eb2-98c9-c81c316d31d3" });
        }
    }
}
