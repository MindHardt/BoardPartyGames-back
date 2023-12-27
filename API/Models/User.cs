namespace API.Models;

/// <summary>
/// 
/// </summary>
/// <param name="Id"></param>
/// <param name="Username"></param>
/// <param name="PasswordHash"></param>
public record UserModel(Guid Id, string Username, string PasswordHash);