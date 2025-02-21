using Business.Helpers;
using Business.Models.Users;
using Data.Entities;

namespace Business.Factories;

public class UserFactory
{
    public static UsersEntity Create(UserRegistrationForm form)
    {
        var (hashedPassword, salt) = PasswordHasher.HashPassword(form.Password);

        return new UsersEntity
        {
            UserName = form.UserName,
            Email = form.Email,
            PasswordHash = hashedPassword,
            PasswordSalt = salt,
            RoleId = form.RoleId
        };

    }

    public static UserForm Create(UsersEntity entity) => new()
    {
        Id = entity.Id,
        UserName = entity.UserName,
        Email = entity.Email,
        RoleId = entity.RoleId
    };

    public static void Update(UsersEntity entity, UserUpdateForm form)
    {
        if (form.Id != entity.Id)
            throw new Exception("Ids do not match");

        if (!string.IsNullOrEmpty(form.UserName))
            entity.UserName = form.UserName;

        if (!string.IsNullOrEmpty(form.Email))
            entity.Email = form.Email;

        if (!string.IsNullOrEmpty(form.Password))
        {
            var (hashedPassword, salt) = PasswordHasher.HashPassword(form.Password);
            entity.PasswordHash = hashedPassword;
            entity.PasswordSalt = salt;
        }

        if (form.RoleId.HasValue)
            entity.RoleId = form.RoleId.Value;
    }

    public static bool VerifyPassword(UsersEntity entity, string password)
    {
        return PasswordHasher.VerifyPassword(password, entity.PasswordHash, entity.PasswordSalt);
    }
        

}
