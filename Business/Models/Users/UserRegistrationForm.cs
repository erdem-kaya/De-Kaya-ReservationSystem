using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Business.Models.Users;

public class UserRegistrationForm
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int RoleId { get; set; }
}


