namespace Application.Codenames.Models;

/// <summary>
/// Represents a newly created game.
/// </summary>
/// <param name="Id">The id of the game used to join to it.</param>
/// <param name="PlayersCount">The total number of players.</param>
public record CodenamesGameModel(Guid Id, int PlayersCount);