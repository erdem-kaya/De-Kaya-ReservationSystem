namespace Business.Models.Payment;

public class PaymentSummary
{
    public int Id { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public decimal? PrePayment { get; set; }
    public decimal RemainingAmount => (Amount - (PrePayment ?? 0));
    public DateTime? ConfirmedAt { get; set; }
    public int PaymentStatusId { get; set; }
    public int BookingId { get; set; }

}
