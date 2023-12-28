﻿using Application.Spyfall.Models;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Application.Codenames.Models;

namespace Application.Spyfall;

/// <summary>
/// A request to get a card in a spyfall game.
/// </summary>
public record GetWordsRequest
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

public class GetWordsHandler(
    DataContext dataContext)
    : IRequestHandler<GetWordsRequest, CodenamesWordsModel>
{
    public async Task<CodenamesWordsModel> HandleAsync(GetWordsRequest request, CancellationToken ct = default)
    {
        // проверяем длину ника
        if (request.Nickname.Length < 3)
            throw new Exception("Nickname must be more then 2 characters");

        // получаем нужный game
        var game = await dataContext.CodenamesGames
            .Include(x => x.Players)
            .FirstOrDefaultAsync(x => x.Id == request.GameId, ct);

        ArgumentNullException.ThrowIfNull(game);

        game.Players ??= new List<CodenamesPlayer>();

        // количество игроков соответствует настройкам комнаты
        if (game.Players.Count >= game.PlayersCount)
        {
            throw new Exception("The limit of players in the room has been exceeded");
        }

        // если игрок с таким ником существует, вернем его
        var playerExist = game.Players?.FirstOrDefault(player => player.Nickname == request.Nickname);
        if (playerExist is not null)
        {
            return playerExist.Role == "cap"
                ? new CodenamesWordsModel(game.Words, playerExist.Color, true, game.Colors)
                : new CodenamesWordsModel(game.Words, playerExist.Color, false, Array.Empty<string>());
        }

        Random random = new Random();

        var newPlayer = new CodenamesPlayer
        {
            Nickname = request.Nickname,
            Role = "player",
            Color = "red"
        };

        int redPlayersCount = game.Players?.Count(x => x.Color == "red") ?? 0;

        if (redPlayersCount <= (int)(game.PlayersCount / 2))
        {
            newPlayer.Color = "red";
            var redCap = game.Players?
                .Where(x => x.Color == "red")
                .Select(x => x.Role)
                .Where(x => x == "cap")
                .ToList();
            if (redCap == null || redCap.Count == 0)
                newPlayer.Role = "cap";
        }
        else
        {
            newPlayer.Color = "blue";
            var blueCap = game.Players?
                .Where(x => x.Color == "blue")
                .Select(x => x.Role)
                .Where(x => x == "cap")
                .ToList();
            if (blueCap == null || blueCap.Count == 0)
                newPlayer.Role = "cap";
        }
            

        var existingGame = await dataContext.CodenamesGames.FindAsync(request.GameId);
        if (existingGame != null)
        {
            existingGame.Players ??= new List<CodenamesPlayer>();
            existingGame.Players.Add(newPlayer);

            await dataContext.SaveChangesAsync(ct);
        }

        return newPlayer.Role == "cap"
            ? new CodenamesWordsModel(game.Words, newPlayer.Color, true, game.Colors)
            : new CodenamesWordsModel(game.Words, newPlayer.Color, false,Array.Empty<string>());
    }
}