using System.ComponentModel;
using Application.Spyfall.Models;

namespace Application.Spyfall;

public record CreateGameRequest(
    [DefaultValue(5)] byte PlayersCount,
    [DefaultValue(null)] byte? SpiesCount = null,
    [DefaultValue(null)] int? RandomSeed = null);

public class CreateGameHandler : IRequestHandler<CreateGameRequest, Game>
{
    public Task<Game> HandleAsync(CreateGameRequest request, CancellationToken ct = default)
    {
        return Task.FromResult(Game.Create(request.PlayersCount, request.SpiesCount, request.RandomSeed));
    }
}