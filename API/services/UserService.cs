using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity.Data;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.services;

public class UserService
{
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;

    public UserService(DataContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }


    public bool Register(User user)
    {
        try
        {
            // Проверяем, не существует ли пользователь с таким именем
            if (_context.Users.Any(u => u.Username == user.Username))
            {
                // В реальном приложении рекомендуется возвращать объект с описанием ошибки или использовать другие механизмы обработки ошибок
                return false;
            }

            user.PasswordHash = HashPassword(user.PasswordHash);

            _context.Users.Add(user);
            _context.SaveChanges(); // Сохранение изменений в базу данных

            return true;
        }
        catch (Exception ex)
        {
            // Обработка ошибок при сохранении в базу данных
            // В реальном приложении логгируйте ошибку и возможно предпримите другие действия
            Console.WriteLine($"Error during user registration: {ex.Message}");
            return false;
        }
    }


    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }

    public User Authenticate(string username, string password)
    {
        return _context.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == HashPassword(password));
    }
}

