using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;

public class UserRepository(DataContext context) : BaseRepository<UsersEntity>(context), IUserRepository
{
    public async Task<bool> AuthenticateAsync(string email, string passwordHash)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return false;

            return user.PasswordHash == passwordHash;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error authenticating user: {ex.Message}");
            return false;
        }
    }
}
