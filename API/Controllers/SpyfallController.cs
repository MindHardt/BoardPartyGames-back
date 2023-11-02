using Application;
using Application.Spyfall;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class SpyfallController : ControllerBase
{
    [HttpPost]
    [EndpointName("CreateGame")]
    [EndpointDescription("Creates a new spyfall game. This should be treated as a game key generator.")]
    public async Task<IActionResult> CreateGame(
        [FromBody] CreateGameRequest request,
        [FromServices] CreateGameHandler handler,
        CancellationToken ct)
    {
        var result = await handler.HandleAsync(request, ct).AsResult();
        return result.Success
            ? Ok(result.Value.GetKey())
            : BadRequest(result.Exception.Message);
    }

    [HttpGet("card")]
    [EndpointName("GetCard")]
    [EndpointDescription("Gets a game card.")]
    public async Task<IActionResult> GetCard(
        [FromQuery] GetCardRequest request,
        [FromServices] GetCardHandler handler,
        CancellationToken ct)
    {
        var result = await handler.HandleAsync(request, ct).AsResult();
        return result.Success
            ? Ok(result.Value)
            : BadRequest(result.Exception.Message);
    }
}