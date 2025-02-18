using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;

public class UserRepository(DataContext context) : BaseRepository<UsersEntity>(context), IUserRepository
{
    public async Task<UsersEntity?> AuthenticateAsync(string email)
    {
        try
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error authenticating user: {ex.Message}");
            return null;
        }
    }
}
