using System.ComponentModel;
using System.Linq;
using Application.Codenames.Models;
using Application.Spyfall;
using Application.Spyfall.Models;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Codenames;


/// <summary>
/// 
/// </summary>
public record SetGuessedWordsRequest
{
    /// <summary>
    /// 
    /// </summary>
    public required Guid GameId { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public required byte WordIndex { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public required bool IsGuessed { get; init; }
}

/// <summary></summary>
public class SetGuessedWordsHandler(
    DataContext dataContext,
    ILogger<SetGuessedWordsRequest> logger)
    : IRequestHandler<SetGuessedWordsRequest, CodenamesIsGuessedModel>
{
    public async Task<CodenamesIsGuessedModel> HandleAsync(SetGuessedWordsRequest request, CancellationToken ct = default)
    {
        var game = await dataContext.CodenamesGames
            .Where(x => x.Id == request.GameId)
            .FirstOrDefaultAsync(cancellationToken: ct);

        try
        {
            if (game != null)
            {
                game.GuessedWords[request.WordIndex] = request.IsGuessed;

                await dataContext.SaveChangesAsync(ct);
            }
            else
            {
                throw new Exception("invalid request");
            }
        }
        catch (Exception e)
        {
            throw new Exception("invalid request");
        }


        return new CodenamesIsGuessedModel(game.GuessedWords);
    }
}