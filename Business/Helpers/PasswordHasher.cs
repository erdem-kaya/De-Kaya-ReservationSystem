using System.Security.Cryptography;
using System.Text;

namespace Business.Helpers;

public static class PasswordHasher
{
    public static (string hash, string salt) HashPassword(string password)
    {
        using var hmac = new HMACSHA512();
        var salt = Convert.ToBase64String(hmac.Key);
        var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password + salt)));
        return (hash, salt);
    }

    public static bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        using var hmac = new HMACSHA512(Convert.FromBase64String(storedSalt));
        var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password + storedSalt)));
        return computedHash == storedHash;
    }
}
