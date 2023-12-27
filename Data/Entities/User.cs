using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public record User : IEntityTypeConfiguration<User>
{
    public Guid Id { get; set; }

    public required string Username { get; set; }
    public required string PasswordHash { get; set; }

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Username).IsRequired();
        builder.Property(u => u.PasswordHash).IsRequired();
    }
}