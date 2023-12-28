namespace Application.Codenames.Models;

/// <summary>
/// 
/// </summary>
/// <param name="Colors"></param>
/// <param name="Words"></param>
/// <param name="TeamColor"></param>
public record CodenamesWordsModel(string[] Words, string TeamColor, bool IsCap, string[]? Colors);