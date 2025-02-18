using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class UsersEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserName { get; set; } = null!;

    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!;

    [Required]
    public string PasswordSalt { get; set; } = null!;

    [Required]
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? LastLogin { get; set; }

    [Required]
    public int RoleId { get; set; }
    public RoleEntity Role { get; set; } = null!;

    public ICollection<CustomersEntity> Customers { get; set; } = [];

}
