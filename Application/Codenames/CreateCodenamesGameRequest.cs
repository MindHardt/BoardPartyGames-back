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
public record CreateCodenamesGameRequest
{
    [DefaultValue("Стандартная")]
    public required string Deck { get; init; }

    /// <summary>
    /// 
    /// </summary>
    [DefaultValue(new[] {"Привет", "Мир"})]
    public required string[] CustomWords { get; init; }

    /// <summary>
    /// A total amount of players in game.
    /// </summary>
    /// <example>5</example>
    public byte PlayersCount { get; init; }


}

/// <summary></summary>
public class CreateCodenamesGameHandler(
    DataContext dataContext,
    ILogger<CreateCodenamesGameRequest> logger)
    : IRequestHandler<CreateCodenamesGameRequest, CodenamesGameModel>
{
    public async Task<CodenamesGameModel> HandleAsync(CreateCodenamesGameRequest request, CancellationToken ct = default)
    {
        var words = await dataContext.CodenamesWords
            .Where(x => x.Deck == request.Deck)
            .Select(x => x.Name)
            .ToListAsync(ct);

        Random rand = new Random();

        List<string> randomWords = words.OrderBy(x => rand.Next()).Take(25 - request.CustomWords.Length).ToList();

        foreach (var x in request.CustomWords)
        {
            int randomIndex = rand.Next(0, randomWords.Count);
            randomWords.Insert(randomIndex, x);
        }

        for (int i = randomWords.Count - 1; i > 0; i--)
        {
            int j = rand.Next(0, i + 1);
            (randomWords[i], randomWords[j]) = (randomWords[j], randomWords[i]);
        }

        bool[] guessedWords = new bool[25];
        for (int i = 0; i < guessedWords.Length; i++)
            guessedWords[i] = false;

        List<string> colors = new List<string>(){ "black", "gray", "gray", "gray", "gray", "gray", "gray", "gray" };
        int redColorsCount = rand.Next(8, 10);
        for (int i = 0; i < redColorsCount; i++)
        {
            int randomIndex = rand.Next(0, colors.Count);
            colors.Insert(randomIndex, "red");
        }

        for (int j = 0; j < 17 - redColorsCount; j++)
        {
            int randomIndex = rand.Next(0, colors.Count);
            colors.Insert(randomIndex, "blue");
        }


        for (int i = colors.Count - 1; i > 0; i--)
        {
            int j = rand.Next(0, i + 1);
            (colors[i], colors[j]) = (colors[j], colors[i]);
        }

        var game = new CodenamesGame
        {
            Words = [.. randomWords],
            PlayersCount = request.PlayersCount
        };

        game.Colors = [.. colors];
        game.GuessedWords = guessedWords;

        dataContext.CodenamesGames.Add(game);
        await dataContext.SaveChangesAsync(ct);

        return new CodenamesGameModel(game.Id, game.PlayersCount);
    }
}