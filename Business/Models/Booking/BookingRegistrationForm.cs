namespace Business.Models.Booking;

public class BookingRegistrationForm
{
    public DateTime BookingDate { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string Description { get; set; } = null!;
    public int CustomerId { get; set; }
    public int CoolingRoomId { get; set; }
    public int BookingStatusId { get; set; }
}
