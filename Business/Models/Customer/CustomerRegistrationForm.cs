using System.ComponentModel.DataAnnotations;

namespace Business.Models.Customer;

public class CustomerRegistrationForm
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Geçerli bir telefon numarası girin.")]
    public string PhoneNumber { get; set; } = null!;
    public int UserId { get; set; }
}
