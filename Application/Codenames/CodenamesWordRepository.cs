using Application;
using Application.Spyfall;
using Application.Spyfall.Models;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

public class CodenamesWordRepository
{
    private readonly DataContext _context;

    public CodenamesWordRepository(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<List<string>> GetDecksAsync()
    {
        return await _context.CodenamesWords.Select(word => word.Deck).Distinct().ToListAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<List<bool>> GetGuessedWordsAsync(Guid id)
    {
        var codenamesGame = _context.CodenamesGames.FirstOrDefaultAsync(x => x.Id == id).Result;
        if (codenamesGame != null)
            return codenamesGame.GuessedWords.ToList();
        throw new Exception("invalid token");
    }
}