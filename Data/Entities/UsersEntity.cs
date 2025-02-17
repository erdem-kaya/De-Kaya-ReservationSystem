using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class UsersEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string PasswordHash { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [Required]
    public int RoleId { get; set; }
    public RoleEntity Role { get; set; } = null!;

    public ICollection<CustomersEntity> Customers { get; set; } = [];
}
