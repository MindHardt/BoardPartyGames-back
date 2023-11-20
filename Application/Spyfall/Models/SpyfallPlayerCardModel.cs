namespace Application.Spyfall.Models;

public record SpyfallPlayerCardModel(string? LocationName, string Role)
{
    public static SpyfallPlayerCardModel Spy => new(null, "Шпион");
}