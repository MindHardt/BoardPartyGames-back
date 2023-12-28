using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Entities;

public record CodenamesPlayer : IEntityTypeConfiguration<CodenamesPlayer>
{
    public Guid Id { get; set; }
    public required string Nickname { get; set; }
    public required string Role { get; set; }
    public required string Color { get; set; }

    public void Configure(EntityTypeBuilder<CodenamesPlayer> builder)
    {
        builder.HasKey(x => x.Id);
    }
}