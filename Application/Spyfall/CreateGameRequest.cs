﻿using System.ComponentModel;
using Application.Spyfall.Models;

namespace Application.Spyfall;

/// <summary>
/// A request to calculate spyfall game key with specified parameters.
/// </summary>
public record CreateGameRequest
{
    /// <summary>
    /// A total amount of players in game.
    /// </summary>
    [DefaultValue(5)]
    public byte PlayersCount { get; init; } = 5;

    /// <summary>
    /// The amount of spies in game. If emitted, will be calculated based on <see cref="PlayersCount"/>.
    /// </summary>
    [DefaultValue(null)]
    public byte? SpiesCount { get; init; } = null;

    /// <summary>
    /// A random seed used to recreate games. If emitted, will be randomized.
    /// </summary>
    [DefaultValue((object?)null)]
    public int? RandomSeed { get; init; }
}

public class CreateGameHandler : IRequestHandler<CreateGameRequest, Game>
{
    public Task<Game> HandleAsync(CreateGameRequest request, CancellationToken ct = default)
    {
        return Task.FromResult(Game.Create(request.PlayersCount, request.SpiesCount, request.RandomSeed));
    }
}