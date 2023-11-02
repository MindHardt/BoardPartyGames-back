using System.ComponentModel;
using Application.Spyfall.Models;

namespace Application.Spyfall;

public record GetCardRequest(
    string Key,
    [DefaultValue(1)] byte Number);

public class GetCardHandler : IRequestHandler<GetCardRequest, PlayerCard>
{
    public Task<PlayerCard> HandleAsync(GetCardRequest request, CancellationToken ct = default)
    {
        var game = Game.FromKey(request.Key);
        var card = game.GetCard((byte)(request.Number - 1));
        return Task.FromResult(card);
    }
}