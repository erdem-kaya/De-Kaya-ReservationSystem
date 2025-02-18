using Business.Models.Users;

namespace Business.Interfaces;

public interface IUserService
{
    Task<UserForm> CreateAsync(UserRegistrationForm form);
    Task<IEnumerable<UserForm>> GetAllAsync();
    Task<UserForm> GetByIdAsync(int id);
    Task<UserForm> UpdateAsync(UserUpdateForm form);
    Task<bool> DeleteAsync(int id);

    // Sifre degistirme islemi icin metot tanimlamasi
    // Change password method definition
    Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword);

    //Kimlik dogrulama islemi icin metot tanimlamasi
    // Method definition for authentication
    Task<UserForm?> AuthenticateAsync(string email, string password);
}
