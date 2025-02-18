namespace Business.Models.Users;

public class UserUpdateForm
{
    public string? UserName { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public string? Password { get; set; } = null!;
    public int? RoleId { get; set; }
}
