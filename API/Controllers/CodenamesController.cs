using Application;
using Application.Codenames;
using Application.Codenames.Models;
using Application.Spyfall;
using Application.Spyfall.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CodenamesController : ControllerBase
{
    private readonly CodenamesWordRepository _wordRepository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="wordRepository"></param>
    public CodenamesController(CodenamesWordRepository wordRepository)
    {
        _wordRepository = wordRepository;
    }

    [HttpPost]
    [EndpointDescription("Creates a new codenames game. This should be treated as a game key generator.")]
    public async Task<ActionResult<CodenamesGameModel>> CreateGame(
        [FromBody] CreateCodenamesGameRequest request,
        [FromServices] CreateCodenamesGameHandler handler,
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
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="handler"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost("setIsGuessed")]
    public async Task<ActionResult<CodenamesIsGuessedModel>> setIsGuessed(
        [FromBody] SetGuessedWordsRequest request,
        [FromServices] SetGuessedWordsHandler handler,
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
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet("decks")]
    [EndpointDescription("Gets list of decks.")]
    public async Task<IActionResult> GetCodenamesDecks()
    {
        var decks = await _wordRepository.GetDecksAsync();
        return Ok(decks);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("guessedWords")]
    public async Task<IActionResult> GetGuessedWords(Guid id)
    {
        var guessedWords = await _wordRepository.GetGuessedWordsAsync(id);
        return Ok(guessedWords);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="handler"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("words")]
    public async Task<ActionResult<CodenamesWordsModel>> GetWords(
        [FromQuery] GetWordsRequest request,
        [FromServices] GetWordsHandler handler,
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