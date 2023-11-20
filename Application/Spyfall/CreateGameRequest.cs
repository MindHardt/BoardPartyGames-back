using System.ComponentModel;
using Application.Spyfall.Models;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Spyfall;

/// <summary>
/// A request to calculate spyfall game key with specified parameters.
/// </summary>
public record CreateGameRequest
{
    /// <summary>
    /// The maximum amount of locations per game.
    /// </summary>
    public const int MaxLocations = 30;

    /// <summary>
    /// The ids of the locations that can be used for this <see cref="SpyfallGame"/>.
    /// Cannot have more than <see cref="MaxLocations"/> elements.
    /// </summary>
    [DefaultValue(new[] { "487fbd48-ff40-42e6-b00f-a5f7410b50c2", "558d22f1-f3c5-42a6-9c7a-9080267bc3f3", "e36c20c7-58b6-4bb6-bd99-36f4c7bbb34b" })]
    public required Guid[] LocationIds { get; init; }

    /// <summary>
    /// A total amount of players in game.
    /// </summary>
    /// <example>5</example>
    public byte PlayersCount { get; init; }

    /// <summary>
    /// The amount of spies in game. If emitted, will be calculated based on <see cref="PlayersCount"/>.
    /// </summary>
    /// <example>null</example>
    public byte? SpiesCount { get; init; } = null;
}

/// <summary></summary>
public class CreateGameHandler(
    DataContext dataContext,
    ILogger<CreateGameHandler> logger)
    : IRequestHandler<CreateGameRequest, SpyfallGameModel>
{
    public async Task<SpyfallGameModel> HandleAsync(CreateGameRequest request, CancellationToken ct = default)
    {
        if (request.SpiesCount >= request.PlayersCount)
        {
            throw new ArgumentException(
                $"{nameof(request.SpiesCount)} cannot be bigger than {nameof(request.PlayersCount)}");
        }

        var spiesCount = request.SpiesCount ?? CalculateSpiesCount(request.PlayersCount);

        var locations = await dataContext.SpyfallLocations
            .Where(x => request.LocationIds.Contains(x.Id))
            .Take(CreateGameRequest.MaxLocations)
            .ToArrayAsync(ct);

        var invalidLocations = locations
            .Where(x => x.Roles.Length < request.PlayersCount - request.SpiesCount);
        if (invalidLocations.Any())
        {
            throw new ArgumentException(
                $"Some of the provided locations aren't valid for this {nameof(request.PlayersCount)}");
        }

        var location = locations[Random.Shared.Next(locations.Length)];
        var allIndices = Enumerable.Range(0, request.PlayersCount)
            .ToArray();
        var spiesIndices = Random.Shared.GetItems(allIndices, request.SpiesCount!.Value);

        var game = new SpyfallGame
        {
            LocationId = location.Id,
            Location = location,

            PlayersCount = request.PlayersCount,
            SpiesIndices = spiesIndices,

            PossibleLocations = locations
        };
        dataContext.SpyfallGames.Add(game);
        await dataContext.SaveChangesAsync(ct);

        logger.LogInformation("Created game with id {Id}", game.Id);

        return new SpyfallGameModel(game.Id, game.PlayersCount, game.SpiesCount);
    }

    private static byte CalculateSpiesCount(byte playersCount)
        => (byte)(playersCount > 6 ? 2 : 1);
}