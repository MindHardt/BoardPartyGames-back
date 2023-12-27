using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities;

public record SpyfallPlayer : IEntityTypeConfiguration<SpyfallPlayer>
{
    public Guid Id { get; set; }
    public required string Nickname { get; set; }
    public required string Role { get; set; }

    public void Configure(EntityTypeBuilder<SpyfallPlayer> builder)
    {
        builder.HasKey(x => x.Id);
    }
}