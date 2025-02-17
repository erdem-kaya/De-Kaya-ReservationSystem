using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class CustomersEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Address { get; set; } = null!;
    [Required]
    public string City { get; set; } = null!;
    [Required]
    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Geçerli bir telefon numarası girin.")]
    public string PhoneNumber { get; set; } = null!;
    [Required]
    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }
    public UsersEntity User { get; set; } = null!;

    public ICollection<BookingsEntity> Bookings { get; set; } = [];
}
