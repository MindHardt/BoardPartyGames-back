using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities;

public record SpyfallGame : IEntityTypeConfiguration<SpyfallGame>
{
    public Guid Id { get; set; }

    public required int PlayersCount { get; set; }
    public required int[] SpiesIndices { get; set; }

    public int SpiesCount => SpiesIndices.Length;

    public required Guid LocationId { get; set; }
    public SpyfallLocation Location { get; set; } = null!;

    public ICollection<SpyfallPlayer>? Players { get; set; }

    public ICollection<SpyfallLocation> PossibleLocations { get; set; } = null!;

    public void Configure(EntityTypeBuilder<SpyfallGame> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Location)
            .WithMany()
            .HasForeignKey(x => x.LocationId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(x => x.PossibleLocations)
            .WithMany()
            .UsingEntity(x => x.ToTable("SpyfallGameLocations"));
        builder.HasMany(x => x.Players)
            .WithMany()
            .UsingEntity(x => x.ToTable("SpyfallGamePlayers"));
    }
}