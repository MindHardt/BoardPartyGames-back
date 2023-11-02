namespace Application.Spyfall.Models;

/// <summary>
/// Represents a spyfall location.
/// </summary>
/// <param name="Name"></param>
/// <param name="Roles"></param>
public record Location(int Id, string Name, IReadOnlyList<string> Roles)
{
    /// <summary>
    /// They are hardcoded for MVP.
    /// </summary>
    public static IReadOnlyList<Location> DefaultLocations { get; } = new[]
    {
        new Location(1, "Заправка", new[]
        {
            "Кассир", "Заправщик", "Дальнобойщик", "Водитель", "Уборщица", "Автомойщик", "Ребенок в кресле", "Кот"
        }),
        new Location(1, "Кафе", new[]
        {
            "Повар", "Официант", "Бармен", "Завсегдатай", "Удаленщик", "Уборщик", "Ребенок", "Толстяк"
        }),
        new Location(1, "Тюрьма", new[]
        {
            "Охранник", "Повар", "Наркоторговец", "Контрабандист", "Убийца", "Врач", "Вор", "Посетитель"
        })
    };
}