using System.Threading.Tasks;
using Lib.UniversalCore.Threading;
using Microsoft.AspNetCore.Mvc;
using Web.MtgDiscovery.Controllers.Results;

namespace Web.MtgDiscovery.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class SearchController : Controller
{
    [HttpGet("Artist")]
    public async Task<IActionResult> GetArtistSearch(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) return Content("[]", "application/json");

        string serialized = await new SearchArtistsResults(searchTerm).Results<string>().NoSync();
        return Content(serialized, "application/json");
    }

    [HttpGet("Card")]
    public async Task<IActionResult> GetCardSearch(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) return Content("[]", "application/json");

        string serialized = await new SearchCardsResults(searchTerm).Results<string>().NoSync();
        return Content(serialized, "application/json");
    }
}
