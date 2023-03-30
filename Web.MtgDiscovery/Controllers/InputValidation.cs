using Lib.UniversalCore.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.MtgDiscovery.Controllers;

public interface IInputValidation
{
    bool TryCardIdProblem(ControllerBase controller, string cardId, out ObjectResult problem);
    bool TrySetIdProblem(ControllerBase controller, string setId, out ObjectResult problem);
    bool TryCollectorInUrlProblem(ControllerBase controller, string collectorInUrl, out ObjectResult problem);
    bool TryUserInputProblem(ControllerBase controller, string userInput, out ObjectResult problem);
}

public sealed class InputValidation : IInputValidation
{
    private const string JavascriptUndefined = "undefined";

    public bool TryCardIdProblem(ControllerBase controller, string cardId, out ObjectResult problem)
    {
        if (cardId.IsNotNullOrWhitespace() && cardId != JavascriptUndefined)
        {
            problem = null;
            return false;
        }

        problem = controller.Problem(type: "/docs/errors/cardId/invalid",
            title: "cardId is Required",
            detail: "cardId is required for this call.",
            statusCode: StatusCodes.Status400BadRequest,
            instance: controller.HttpContext.Request.Path);
        return true;
    }

    public bool TrySetIdProblem(ControllerBase controller, string setId, out ObjectResult problem)
    {
        if (setId.IsNotNullOrWhitespace() && setId != JavascriptUndefined)
        {
            problem = null;
            return false;
        }

        problem = controller.Problem(type: "/docs/errors/setId/invalid",
            title: "setId is Required",
            detail: "setId is required for this call.",
            statusCode: StatusCodes.Status400BadRequest,
            instance: controller.HttpContext.Request.Path);
        return true;
    }

    public bool TryCollectorInUrlProblem(ControllerBase controller, string collectorInUrl, out ObjectResult problem)
    {
        if (collectorInUrl.IsNotNullOrWhitespace() && collectorInUrl != JavascriptUndefined)
        {
            problem = null;
            return false;
        }

        problem = controller.Problem(type: "/docs/errors/collectorInUrl/invalid",
            title: "collectorInUrl is Required",
            detail: "collectorInUrl is required for this call.",
            statusCode: StatusCodes.Status400BadRequest,
            instance: controller.HttpContext.Request.Path);
        return true;
    }

    public bool TryUserInputProblem(ControllerBase controller, string userInput, out ObjectResult problem)
    {
        if (userInput.IsNotNullOrWhitespace() && userInput != JavascriptUndefined)
        {
            problem = null;
            return false;
        }

        problem = controller.Problem(type: "/docs/errors/userInput/invalid",
            title: "userInput is Required",
            detail: "userInput is required for this call.",
            statusCode: StatusCodes.Status400BadRequest,
            instance: controller.HttpContext.Request.Path);
        return true;
    }
}
