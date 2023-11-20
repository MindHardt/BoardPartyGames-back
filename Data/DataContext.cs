using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<SpyfallGame> SpyfallGames => Set<SpyfallGame>();
    public DbSet<SpyfallLocation> SpyfallLocations => Set<SpyfallLocation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
}