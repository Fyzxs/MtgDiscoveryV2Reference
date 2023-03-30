using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.MtgDiscovery.Views.AllSets.Partial
{
    public sealed class SetLoadingModel : PageModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string ReleasedAt { get; set; }
        public string BaseSetSize { get; set; }
        public string ParentSetCode { get; set; }
        public string OriginalCode { get; set; }

        public void OnGet()
        {
        }
    }
}
