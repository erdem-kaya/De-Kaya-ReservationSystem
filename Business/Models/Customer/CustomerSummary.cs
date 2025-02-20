namespace Business.Models.Customer;

public class CustomerSummary
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string City { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int BookingCount { get; set; }
}
