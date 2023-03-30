using System.Threading.Tasks;
using Lib.MtgDiscovery.Primitives.Core.Cards;
using Lib.MtgDiscovery.Primitives.Core.Collectors;
using Lib.MtgDiscovery.Primitives.Core.Sets;
using Lib.UniversalCore.Extensions;
using Lib.UniversalCore.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.MtgDiscovery.Caching.Keys;
using Web.MtgDiscovery.Controllers.Results;
using Web.MtgDiscovery.Databases.AzureSql.Commands.Collectors;
using Web.MtgDiscovery.Databases.AzureSql.Commands.SprocArgs;
using Web.MtgDiscovery.Databases.Cosmos.Queries.DataBags;
using Web.MtgDiscovery.Pages.Shared;
using Web.MtgDiscovery.UserStuff;
using Web.MtgDiscovery.Views.AllSets.Partial;
using Web.MtgDiscovery.Views.Collector.Partial;

namespace Web.MtgDiscovery.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class CollectorController : Controller
{
    private static readonly IInputValidation s_validation = new InputValidation();

    [HttpGet("AdjustCard")]
    public async Task<IActionResult> AdjustCardInCollection(string cardId, string setId, string userInput, string collectorInUrl)
    {
        if (s_validation.TryCardIdProblem(this, cardId, out ObjectResult problemCardId)) return problemCardId;
        if (s_validation.TrySetIdProblem(this, setId, out ObjectResult problemSetId)) return problemSetId;
        if (s_validation.TryUserInputProblem(this, userInput, out ObjectResult problemUserInput)) return problemUserInput;
        if (s_validation.TryCollectorInUrlProblem(this, collectorInUrl, out ObjectResult problemCollector)) return problemCollector;

        if (string.IsNullOrWhiteSpace(userInput)) return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Update Input invalid");

        CardId sCardId = new StringCardId(cardId);
        SetId sSetId = new StringSetId(setId);
        CollectorId collectorId = new StringCollectorId(collectorInUrl);

        IAuthnDetails userDetails = new AuthnUserDetails(User);
        if (userDetails.IsNotAuthenticated() || userDetails.CollectorId() != collectorInUrl) return Problem(
            type: "/docs/errors/forbidden",
            title: "Authenticated user is not authorized to update target collection",
            detail: "Feature Not Available",
            statusCode: StatusCodes.Status403Forbidden,
            instance: HttpContext.Request.Path
        );

        await new CommandCollectorCardAzureCommand().ExecuteAsync(new UpsertCollectorCardSprocArgs(collectorId, sSetId, sCardId, userInput));

        CacheController.ClearCacheItem(new CollectorCardCountDetailsCacheKey(sCardId, collectorId));
        CacheController.ClearCacheItem(new CollectorAllSetsDetailsCacheKey(collectorId));
        CacheController.ClearCacheItem(new CollectorSetAllCardsCountDetailsCacheKey(sSetId, collectorId));
        CacheController.ClearCacheItem(new CollectorSetCountDetailsCacheKey(sSetId, collectorId));
        CacheController.ClearCacheItem(new CollectorSetCardCountDetailsCacheKey(sSetId, collectorId));

        return Accepted();
    }

    [HttpGet("CardCounts")]
    public async Task<IActionResult> CardCounts(string cardId, string collectorInUrl)
    {
        if (s_validation.TryCardIdProblem(this, cardId, out ObjectResult problemCardId)) return problemCardId;
        if (s_validation.TryCollectorInUrlProblem(this, collectorInUrl, out ObjectResult problemCollector)) return problemCollector;

        CollectorId collectorId = new StringCollectorId(collectorInUrl);

        if (collectorId.IsNotValid()) return PartialView("_CardCountsPartial", new CardCountsModel());

        string model = await new CollectorCardCountDetailsResults(cardId, collectorInUrl).Results<string>().NoSync();

        CardCountsModel cardCountsModel = JsonConvert.DeserializeObject<CardCountsModel>(model)!;
        cardCountsModel.HasCollector = true;

        return PartialView("_CardCountsPartial", cardCountsModel);
    }

    [HttpGet("SetCollection")]
    public async Task<IActionResult> SetCollection(string setId, string setCode, string collectorInUrl)
    {
        if (s_validation.TrySetIdProblem(this, setId, out ObjectResult problemSetId)) return problemSetId;
        if (collectorInUrl.IsNullOrWhitespace()) return Content("");

        Task<SetDisplayCacheModel> setDisplayCacheModelTask = new SetDisplayDetailsResults(setCode).Results<SetDisplayCacheModel>();
        Task<CollectorSetSelfSerialize> collectorSetSelfSerializeTask = new CollectorSetAllCardsCountDetailsResults(setId, collectorInUrl).Results<CollectorSetSelfSerialize>();

        await Task.WhenAll(setDisplayCacheModelTask, collectorSetSelfSerializeTask).NoSync();

        SetDisplayCacheModel setInfoModel = await setDisplayCacheModelTask;
        CollectorSetSelfSerialize collectorSet = await collectorSetSelfSerializeTask;

        SetCollectionCacheModel model = new SetCollectionCacheModel
        {
            BaseSetSize = setInfoModel.BaseSetSize,
            Code = setInfoModel.Code,
            Id = setInfoModel.Id,
            SetName = setInfoModel.Name,
            SetCollectionCount = collectorSet.UniqueCount,
            SetCardsCount = collectorSet.Total
        };

        return PartialView("Partial/_SetCollectionPartial", model);
    }

    [HttpGet("CollectorAllSets")]
    public async Task<IActionResult> AllSetsCollection(string collectorInUrl)
    {
        string model = await new CollectorAllSetsDetailsResults(collectorInUrl).Results<string>().NoSync();

        return Content(model, "application/json");
    }
}
