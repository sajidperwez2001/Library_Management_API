using Library.Api.Data;
using Library.Api.Models;
using Microsoft.EntityFrameworkCore;
namespace Library.Api.Services;
public class AuthService : IAuthService
{
    private readonly LibraryContext _db;
    public AuthService(LibraryContext db) { _db = db; }
    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.Username == username);
        if (user == null) return null;
        if (user.PasswordHash != password) return null; // demo only
        return user;
    }
    public async Task<User?> RegisterAsync(string username, string password, string role)
    {
        if (await _db.Users.AnyAsync(u => u.Username == username)) return null;
        var user = new User { Username = username, PasswordHash = password, Role = role };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }
}
