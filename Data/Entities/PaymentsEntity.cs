using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class PaymentsEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public DateTime PaymentDate { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }
    [Required]
    public string PaymentMethod { get; set; } = null!;
    [Column(TypeName = "decimal(18,2)")]
    public decimal? PrePayment { get; set; }
    public DateTime? ConfirmedAt { get; set; }

    [Required]
    public int PaymentStatusId { get; set; }
    public PaymentStatusEntity PaymentStatus { get; set; } = null!;

    [Required]
    public int BookingId { get; set; }
    public BookingsEntity Booking { get; set; } = null!;



}
