﻿using Application.Spyfall.Models;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Application.Spyfall;

/// <summary>
/// A request to get a card in a spyfall game.
/// </summary>
public record GetCardRequest
{
    /// <summary>
    /// A game key generated by POST /game
    /// </summary>
    public required Guid GameId { get; init; }
    /// <summary>
    /// A 1-based player number.
    /// </summary>
    /// <example>1</example>
    public required string Nickname { get; init; }
}

public class GetCardHandler(
    DataContext dataContext)
    : IRequestHandler<GetCardRequest, SpyfallPlayerCardModel>
{
    public async Task<SpyfallPlayerCardModel> HandleAsync(GetCardRequest request, CancellationToken ct = default)
    {
        var game = await dataContext.SpyfallGames
            .Include(x => x.Location)
            .FirstOrDefaultAsync(x => x.Id == request.GameId, ct);

        ArgumentNullException.ThrowIfNull(game);

        var roles = game.Location.Roles
            .Cast<string>()
            .ToList();

        string randomRole = "";
        Random random = new Random();

        game.Players ??= new List<SpyfallPlayer>();

        List<string> usedRoles = game.Players?.Select(x => x.Role)?.ToList() ?? new List<string>();
        if (usedRoles.Count == 0)
        {
            randomRole = roles[random.Next(roles.Count)];
        }
        else
        {
            List<string> unusedRoles = roles.Except(usedRoles).ToList();
            randomRole = unusedRoles[random.Next(unusedRoles.Count)];
        }

        var newPlayer = new SpyfallPlayer
        {
            Nickname = request.Nickname,
            Role = randomRole
        };

        int spies = game.Players?.Count(x => x.Role == "spy") ?? 0;
        if (game.SpiesCount > spies)
        {
            bool isSpy = random.Next(2) == 0;
            if (isSpy || (game.SpiesCount - spies) >= game.PlayersCount - (game.Players?.Count() ?? 0))
            {
                newPlayer.Role = "spy";
            }
        }

        var existingGame = dataContext.SpyfallGames.Find(request.GameId);
        if (existingGame != null)
        {
            existingGame.Players ??= new List<SpyfallPlayer>();
            existingGame.Players.Add(newPlayer);

            await dataContext.SaveChangesAsync();
        }

        return newPlayer.Role is "spy"
            ? SpyfallPlayerCardModel.Spy
            : new SpyfallPlayerCardModel(game.Location.Name, newPlayer.Role);
    }
}