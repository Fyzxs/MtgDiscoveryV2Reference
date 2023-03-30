using System.Threading.Tasks;
using Lib.MtgDiscovery.Primitives.Core.Collectors;
using Lib.UniversalCore.Threading;
using Microsoft.AspNetCore.Mvc;
using Web.MtgDiscovery.Controllers.Results;
using Web.MtgDiscovery.Pages.Shared;
using Web.MtgDiscovery.UserStuff;
using Web.MtgDiscovery.Views.Cards.Partial;

namespace Web.MtgDiscovery.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public sealed class CardsController : Controller
{
    private static readonly IInputValidation s_validation = new InputValidation();

    [HttpGet("Loading")]
    public IActionResult GetLoading(string code, string id, string name, string collectorNumber, string rarity, int total, string price, string releasedAt)
    {
        return PartialView("Partial/_CardLoadingPartial", new CardLoadingModel()
        {
            Id = id,
            Code = code,
            Name = name,
            CollectorNumber = collectorNumber,
            Rarity = rarity,
            Total = total,
            Price = price,
            ReleasedAt = releasedAt
        });
    }

    [HttpGet("SetCardsSummary")]
    public async Task<IActionResult> GetSetCardsSummary(string setCode, string collectorInUrl)
    {
        CollectorId collectorId = new StringCollectorId(collectorInUrl);

        Task<string> setIdTask = new SetMapCodeToIdResults(setCode).Results<string>();
        string summaryModel = await new SetCardsLoadingDetailsResults(setCode).Results<string>().NoSync();
        if (collectorId.IsNotValid()) return Content($"[{summaryModel},[]]", "application/json");
        string setId = await setIdTask.NoSync();

        string cardsCounts = await new CollectorSetCardCountDetailsResults(setId, collectorInUrl).Results<string>().NoSync();

        return Content($"[{summaryModel},{cardsCounts}]", "application/json");
    }

    [HttpGet("ArtistCardsSummary")]
    public async Task<IActionResult> GetArtistCardsSummary(string artist)
    {
        string summaryModel = await new ArtistCardsLoadingDetailsResults(artist).Results<string>().NoSync();
        //TODO: Get counts on load
        return Content($"[{summaryModel},[]]", "application/json");
    }

    [HttpGet("CardVersionsSummary")]
    public async Task<IActionResult> GetCardVersionsSummary(string cardName)
    {
        string summaryModel = await new CardVersionsSummaryResults(cardName).Results<string>().NoSync();
        //TODO: Get counts on load
        return Content($"[{summaryModel},[]]", "application/json");
    }

    [HttpGet("Display")]
    public async Task<IActionResult> GetDisplay(string id, string collectorInUrl)
    {
        CollectorId collector = new StringCollectorId(collectorInUrl);
        AuthnUserDetails authnUserDetails = new(User);

        CardDisplayModel model = await new CardDisplayDetailsResults(id).Results<CardDisplayModel>().NoSync();

        model.IsUrlCollector = collector.IsValid() && authnUserDetails.CollectorId() == collector.AsSystemType();

        if (collector.IsNotValid()) return PartialView("Partial/_CardDisplayPartial", model);

        if (s_validation.TryCardIdProblem(this, id, out ObjectResult problemCardId)) return problemCardId;

        CardCountsModel cardCountsModel = await new CollectorCardCountDetailsResults(id, collectorInUrl).Results<CardCountsModel>().NoSync();
        cardCountsModel.HasCollector = true;
        model.CountsModel = cardCountsModel;
        model.AlteredCount = cardCountsModel.ArtistAltered;
        model.ArtistProofCount = cardCountsModel.ArtistProof;
        model.UnmodifiedCount = cardCountsModel.Owned;
        model.SignedCount = cardCountsModel.Signed;
        model.Collector = collectorInUrl;

        return PartialView("Partial/_CardDisplayPartial", model);
    }
}
