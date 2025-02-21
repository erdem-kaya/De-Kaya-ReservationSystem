namespace Business.Models.Booking;

public class BookingUpdateForm
{
    public int Id { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string Description { get; set; } = null!;
    public int BookingStatusId { get; set; }
}
