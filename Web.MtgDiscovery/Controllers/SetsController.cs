using System.Threading.Tasks;
using Lib.MtgDiscovery.Primitives.Core.Collectors;
using Lib.UniversalCore.Threading;
using Microsoft.AspNetCore.Mvc;
using Web.MtgDiscovery.Controllers.Results;

namespace Web.MtgDiscovery.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class SetsController : Controller
{
    [HttpGet("AllSetCodes")]
    public async Task<IActionResult> GetAllSetCodes(string collectorInUrl)
    {
        string setsModel = await new AllSetsLoadingDetailsResults().Results<string>().NoSync();

        CollectorId collectorId = new StringCollectorId(collectorInUrl);

        if (collectorId.IsNotValid()) return Content($"[{setsModel},[]]", "application/json");

        string collectorModel = await new CollectorAllSetsDetailsResults(collectorInUrl).Results<string>().NoSync();

        return Content($"[{setsModel},{collectorModel}]", "application/json");
    }
}
