using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities;

public record SpyfallLocation : IEntityTypeConfiguration<SpyfallLocation>
{
    public const int MaxNameLength = 64;

    public Guid Id { get; set; }

    [MaxLength(MaxNameLength)]
    public required string Name { get; set; }
    public required string ImageUrl { get; set; }
    public required string[] Roles { get; set; }
    public required string Deck { get; set; }

    public void Configure(EntityTypeBuilder<SpyfallLocation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Name)
            .IsUnique();
        builder.HasData(SeededValues);
    }

    private static SpyfallLocation[] SeededValues =
    {
        new()
        {
            Id = Guid.Parse("558d22f1-f3c5-42a6-9c7a-9080267bc3f3"),
            Name = "Заправка",
            ImageUrl = "img/Spyfall/Заправка.jpg",
            Roles = new[]
            {
                "Кассир", "Заправщик", "Дальнобойщик", "Водитель", "Уборщица", "Автомойщик", "Ребенок в кресле", "Кот"
            },
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("e36c20c7-58b6-4bb6-bd99-36f4c7bbb34b"),
            Name = "Кафе",
            ImageUrl = "img/Spyfall/Кафе.jpg",
            Roles = new[]
            {
                "Повар", "Официант", "Бармен", "Завсегдатай", "Удаленщик", "Уборщик", "Ребенок", "Толстяк"
            },
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("487fbd48-ff40-42e6-b00f-a5f7410b50c2"),
            Name = "Тюрьма",
            ImageUrl = "img/Spyfall/Тюрьма.jpg",
            Roles = new[]
            {
                "Охранник", "Повар", "Наркоторговец", "Контрабандист", "Убийца", "Врач", "Вор", "Посетитель"
            },
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("547fcd48-ff40-42e6-d10d-a5f7410b30f5"),
            Name = "Поезд",
            ImageUrl = "img/Spyfall/Поезд.jpg",
            Roles = new[]
            {
                "Охранник", "Проводник", "Наркоторговец", "Контрабандист", "Убийца", "Повор", "Пассажир"
            },
            Deck = "premium gold VIP deck"
        }
    };
}