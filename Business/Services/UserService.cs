using Business.Factories;
using Business.Helpers;
using Business.Interfaces;
using Business.Models.Users;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<UserForm?> AuthenticateAsync(string email, string password)
    {
        try
        {
            var user = await _userRepository.AuthenticateAsync(email) ?? throw new UnauthorizedAccessException("User not found.");

            // Şifre doğrulama burada yapılıyor
            // Password verification is done here
            var isPasswordValid = PasswordHasher.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
            if (!isPasswordValid)
                throw new UnauthorizedAccessException("Invalid credentials.");

            return UserFactory.Create(user);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error authenticating user: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> ChangePasswordAsync(int userId, string currentPassword, string newPassword)
    {
        try
        {
            var user = await _userRepository.GetAsync(u => u.Id == userId) ?? throw new Exception($"User with ID {userId} not found.");
            var isCurrentPasswordValid = PasswordHasher.VerifyPassword(currentPassword, user.PasswordHash, user.PasswordSalt);
            if (!isCurrentPasswordValid)
                throw new UnauthorizedAccessException("Current password is incorrect.");

            var (hashedPassword, salt) = PasswordHasher.HashPassword(newPassword);
            user.PasswordHash = hashedPassword;
            user.PasswordSalt = salt;

            var updatedUser = await _userRepository.UpdateAsync(user.Id, user);
            return updatedUser != null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error changing password: {ex.Message}");
            return false;
        }
    }

    public async Task<UserForm> CreateAsync(UserRegistrationForm form)
    {
        if (form == null)
            return null!;
        await _userRepository.BeginTransactionAsync();

        try
        {
            var userEntity = UserFactory.Create(form);
            var createdUser = await _userRepository.CreateAsync(userEntity);
                await _userRepository.CommitTransactionAsync();
            return createdUser != null ? UserFactory.Create(createdUser) : null!;
        }
        catch (Exception ex)
        {
            await _userRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating user entity : {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<UserForm>> GetAllAsync()
    {
        try
        {
            var allUsers = await _userRepository.GetAllAsync();
            var result = allUsers.Select(UserFactory.Create).ToList();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all users: {ex.Message}");
            return null!;
        }
    }

    public async Task<UserForm> GetByIdAsync(int id)
    {
        try
        {
            var getUserWithId = await _userRepository.GetAsync(u => u.Id == id);
            var result = getUserWithId != null ? UserFactory.Create(getUserWithId) : null!;
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting user by id: {ex.Message}");
            return null!;
        }
    }


    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var getUserWithId = await _userRepository.GetAsync(u => u.Id == id) ?? throw new Exception($"User with ID {id} does not exist.");

            var deletedUser = await _userRepository.DeleteAsyncById(id);
            if (!deletedUser)
                throw new Exception($"Error deleting user with ID {id}");
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting user: {ex.Message}");
            return false;
        }
    }
      
    public async Task<UserForm> UpdateAsync(UserUpdateForm form)
    {
        if (form == null)
            return null!;
        try
        {
            var findUser = await _userRepository.GetAsync(u => u.Id == form.Id) ?? throw new Exception($"User with ID {form.Id} does not exist.");
            UserFactory.Update(findUser, form);
            var updatedUser = await _userRepository.UpdateAsync(form.Id, findUser);
            return updatedUser != null ? UserFactory.Create(updatedUser) : null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating user: {ex.Message}");
            return null!;
        }
    }
}
