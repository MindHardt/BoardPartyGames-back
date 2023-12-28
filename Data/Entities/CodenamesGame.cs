using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities;

public record CodenamesGame : IEntityTypeConfiguration<CodenamesGame>
{
    public Guid Id { get; set; }

    public required int PlayersCount { get; set; }

    public ICollection<CodenamesPlayer>? Players { get; set; }

    public string[] Colors { get; set; } = [];

    public bool[] GuessedWords { get; set; } = [];

    public string[] Words { get; set; } = null!;

    public void Configure(EntityTypeBuilder<CodenamesGame> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Players)
            .WithMany()
            .UsingEntity(x => x.ToTable("CodenamesGamePlayers"));
    }
}