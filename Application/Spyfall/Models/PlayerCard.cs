namespace Application.Spyfall.Models;

public record PlayerCard(string? LocationName, string Role)
{
    public static PlayerCard Spy => new(null, "Шпион");
}