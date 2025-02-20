using System.ComponentModel.DataAnnotations;

namespace Business.Models.Customer;

public class CustomerUpdateForm
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Geçerli bir telefon numarası girin.")]
    public string? PhoneNumber { get; set; }
    public int? UserId { get; set; }
}
