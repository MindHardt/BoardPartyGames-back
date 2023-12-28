using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities;

public record CodenamesWord : IEntityTypeConfiguration<CodenamesWord>
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string ImageUrl { get; set; } = "";
    public required string Deck { get; set; }

    public void Configure(EntityTypeBuilder<CodenamesWord> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasData(SeededValues);
    }

    private static CodenamesWord[] SeededValues =
    {
        new()
        {
            Id = Guid.Parse("e36c20c7-58b6-4bb6-bd99-36f4c7bbb34f"),
            Name = "Кафе",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("487fbd48-ff40-42e6-b00f-a5f7410b50c3"),
            Name = "Тюрьма",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("547fcd48-ff40-42e6-d10d-a5f7410b30f1"),
            Name = "Поезд",
            Deck = "premium gold VIP deck"
        },
        new()
        {
            Id = Guid.Parse("658d22f1-f3c5-42a6-9c7a-9080267bc3fc"),
            Name = "Заправка",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("736c20c7-58b6-4bb6-bd99-36f4c7bbb322"),
            Name = "Кафе",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("887fbd48-ff40-42e6-b00f-a5f7410b50c3"),
            Name = "Тюрьма",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("958d22f1-f3c5-42a6-9c7a-9080267bc3f3"),
            Name = "Заправка",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("106c20c7-58b6-4bb6-bd99-36f4c7bbb347"),
            Name = "Кафе",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("117fbd48-ff40-42e6-b00f-a5f7410b50c4"),
            Name = "Тюрьма",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("128d22f1-f3c5-42a6-9c7a-9080267bc3f3"),
            Name = "Заправка",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("136c20c7-58b6-4bb6-bd99-36f4c7bbb34f"),
            Name = "Кафе",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("147fbd48-ff40-42e6-b00f-a5f7410b50c9"),
            Name = "Тюрьма",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("158d22f1-f3c5-42a6-9c7a-9080267bc3ab"),
            Name = "Заправка",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("166c20c7-58b6-4bb6-bd99-36f4c7bbb34b"),
            Name = "Кафе",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("177fbd48-ff40-42e6-b00f-a5f7410b50c2"),
            Name = "Тюрьма",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("188d22f1-f3c5-42a6-9c7a-9080267bc3f3"),
            Name = "Заправка",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("196c20c7-58b6-4bb6-bd99-36f4c7bbb34b"),
            Name = "Кафе",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("207fbd48-ff40-42e6-b00f-a5f7410b50c2"),
            Name = "Тюрьма",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("218d22f1-f3c5-42a6-9c7a-9080267bc3f3"),
            Name = "Заправка",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("226c20c7-58b6-4bb6-bd99-36f4c7bbb34b"),
            Name = "Кафе",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("237fbd48-ff40-42e6-b00f-a5f7410b50c2"),
            Name = "Тюрьма",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("248d22f1-f3c5-42a6-9c7a-9080267bc3f3"),
            Name = "Заправка",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("256c20c7-58b6-4bb6-bd99-36f4c7bbb34b"),
            Name = "Кафе",
            Deck = "Стандартная"
        },
        new()
        {
            Id = Guid.Parse("487fbd48-fc40-46e6-b10f-a5f7410b50c3"),
            Name = "Тюрьма",
            Deck = "Стандартная"
        }
    };
}