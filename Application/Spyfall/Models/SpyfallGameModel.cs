namespace Application.Spyfall.Models;

/// <summary>
/// Represents a newly created game.
/// </summary>
/// <param name="Id">The id of the game used to join to it.</param>
/// <param name="PlayersCount">The total number of players.</param>
/// <param name="SpiesCount">The amount of spies in this game.</param>
public record SpyfallGameModel(Guid Id, int PlayersCount, int SpiesCount);