using Api.Models;
using Api.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace Api.Services;

public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public User Register(string email, string password)
    {
        var existing = _repository.GetByEmail(email);
        if (existing != null)
            throw new InvalidOperationException("Email already in use.");
        //error checking
        var user = new User
        {
            Email = email,
            PasswordHash = HashPassword(password)
        };

        _repository.Add(user);
        return user;
    }
    public User? Login(string email, string password)
    {
        var user = _repository.GetByEmail(email);
        if (user == null)
            return null;

        var hash = HashPassword(password);

        if (user.PasswordHash != hash)
            return null;

        return user;
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
