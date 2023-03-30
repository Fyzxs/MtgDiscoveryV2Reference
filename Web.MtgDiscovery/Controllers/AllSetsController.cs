using System.Threading.Tasks;
using Lib.UniversalCore.Extensions;
using Lib.UniversalCore.Threading;
using Microsoft.AspNetCore.Mvc;
using Web.MtgDiscovery.Controllers.Results;
using Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags;
using Web.MtgDiscovery.Views.AllSets.Partial;

namespace Web.MtgDiscovery.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class AllSetsController : Controller
{
    private static readonly IInputValidation s_validation = new InputValidation();

    [HttpGet("Loading2")]
    public IActionResult GetLoading2()
    {
        return PartialView("_SetLoadingPartial");
    }

    [HttpGet("DisplayLimited")]
    public async Task<IActionResult> GetDisplayLimited(string setCode, string collectorInUrl)
    {
        string setId = await new SetMapCodeToIdResults(setCode).Results<string>().NoSync();

        return await GetDisplay(setId, setCode, collectorInUrl).NoSync();
    }

    [HttpGet("Display")]
    public async Task<IActionResult> GetDisplay(string setId, string setCode, string collectorInUrl)
    {
        if (collectorInUrl.IsNullOrWhitespace()) return await GetDisplayWithOutCollector(setCode);
        return await GetDisplayWithCollector(setId, setCode, collectorInUrl);
    }

    public async Task<IActionResult> GetDisplayWithOutCollector(string setCode)
    {
        Task<SetDisplayCacheModel> setDisplayResultsTask = new SetDisplayDetailsResults(setCode).Results<SetDisplayCacheModel>();
        SetDisplayCacheModel setDisplayCacheModel = await setDisplayResultsTask.NoSync();

        return PartialView("Partial/_SetDisplayPartial", setDisplayCacheModel);
    }

    public async Task<IActionResult> GetDisplayWithCollector(string setId, string setCode, string collectorInUrl)
    {
        if (s_validation.TrySetIdProblem(this, setId, out ObjectResult problemSetId)) return problemSetId;
        if (s_validation.TryCollectorInUrlProblem(this, collectorInUrl, out ObjectResult problemCollector)) return problemCollector;
        //Apologies(qgil, 20220903) - It's a bit procedural
        //Note: The issue here is that the Page Model is being used as deserialized into
        //There needs to be a version of a PageModel that encapsulates the two IResults here
        /*
         * class SetDisplayModelResult:ToSystemType<SetDisplayPageModel>{
         *  ctor():this(new SetDisplayResults(), new CollectorSetAllCards()){}
         *
         *  AsSystemType(){
         *      The rest of the below code
         *      return new DataBag{
         *          props = values;
         *          }
         * }
         *
         * used as new ResultsSetDisplayModel(args).AsSystemType()
         */

        Task<SetDisplayCacheModel> setDisplayResultsTask = new SetDisplayDetailsResults(setCode).Results<SetDisplayCacheModel>();
        Task<CollectorSetSelfSerialize> collectorSetAllCardsTask = new CollectorSetAllCardsCountDetailsResults(setId, collectorInUrl).Results<CollectorSetSelfSerialize>();

        await Task.WhenAll(setDisplayResultsTask, collectorSetAllCardsTask).NoSync();

        SetDisplayCacheModel setDisplayCacheModel = await setDisplayResultsTask.NoSync();
        CollectorSetSelfSerialize collectorSetSelfSerialize = await collectorSetAllCardsTask.NoSync();

        setDisplayCacheModel.SetCardsCount = collectorSetSelfSerialize.Total;
        setDisplayCacheModel.SetCollectionCount = collectorSetSelfSerialize.UniqueCount;
        setDisplayCacheModel.Collector = collectorInUrl;

        return PartialView("Partial/_SetDisplayPartial", setDisplayCacheModel);
    }
}
