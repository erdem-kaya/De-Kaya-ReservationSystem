namespace Business.Models.Booking;

public class BookingSummary
{
    public int Id { get; set; }
    public DateTime BookingDate { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string Description { get; set; } = null!;
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public int CoolingRoomId { get; set; }
    public string CoolingRoomName { get; set; } = null!;
    public int BookingStatusId { get; set; }
    public string BookingStatus { get; set; } = null!;
    public bool IsFullyPaid { get; set; }
}