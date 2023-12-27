using Application;
using Application.Spyfall;
using Application.Spyfall.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class SpyfallController : ControllerBase
{
    /// <summary>
    /// Calculates a key for a spyfall game with specified parameters.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="handler"></param>
    /// <param name="ct"></param>
    /// <returns>A string key of a spyfall game. It will look like ACG5-AB15-041F</returns>
    [HttpPost]
    [EndpointName("CreateGame")]
    [EndpointDescription("Creates a new spyfall game. This should be treated as a game key generator.")]
    public async Task<ActionResult<SpyfallGameModel>> CreateGame(
        [FromBody] CreateGameRequest request,
        [FromServices] CreateGameHandler handler,
        CancellationToken ct)
    {
        var result = await handler
            .HandleAsync(request, ct)
            .AsResult();
        return result.Success
            ? result.Value
            : BadRequest(result.Exception.Message);
    }

    /// <summary>
    /// Gets a game card of a user with specified number in a game with given key.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="handler"></param>
    /// <param name="ct"></param>
    /// <returns>A json with location and role. If role is a "Spy" then location is set to null.</returns>
    [HttpGet("card")]
    [EndpointName("GetCard")]
    [EndpointDescription("Gets a game card.")]
    public async Task<ActionResult<SpyfallPlayerCardModel>> GetCard(
        [FromQuery] GetCardRequest request,
        [FromServices] GetCardHandler handler,
        CancellationToken ct)
    {
        var result = await handler.HandleAsync(request, ct).AsResult();
        return result.Success
            ? result.Value
            : result.Exception is ArgumentNullException
                ? NotFound()
                : BadRequest(result.Exception.Message);
    }
}