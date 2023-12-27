using Application;
using Application.Spyfall;
using Application.Spyfall.Models;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

public class Location
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
}

public class SpyfallLocationRepository
{
    private readonly DataContext _context;

    public SpyfallLocationRepository(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<Location>> GetLocationsAsync()
    {
        var locations = await _context.SpyfallLocations.ToListAsync();

        var locationsWithoutRoles = locations.Select(location => new Location
        {
            Id = location.Id,
            Name = location.Name,
            ImageUrl = location.ImageUrl
        }).ToList();
        return locationsWithoutRoles;
    }

    public async Task<List<string>> GetDecksAsync()
    {
        return await _context.SpyfallLocations.Select(location => location.Deck).Distinct().ToListAsync();
    }
}