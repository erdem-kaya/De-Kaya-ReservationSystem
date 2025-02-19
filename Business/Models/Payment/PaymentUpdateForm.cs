namespace Business.Models.Payment;

public class PaymentUpdateForm
{
    public int Id { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public decimal? PrePayment { get; set; }
    public DateTime? ConfirmedAt { get; set; }
    public int PaymentStatusId { get; set; }
    public int BookingId { get; set; }
}
