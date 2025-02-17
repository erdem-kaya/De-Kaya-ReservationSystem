using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class BookingsEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public DateTime BookingDate { get; set; }
    [Required]
    public DateTime CheckInDate { get; set; }
    [Required]
    public DateTime CheckOutDate { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalPrice { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
   
    public DateTime? UpdatedAt { get; set; }
    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public int CustomerId { get; set; }
    public CustomersEntity Customer { get; set; } = null!;

    [Required]
    public int CoolingRoomId { get; set; }
    public CoolingRoomEntity CoolingRoom { get; set; } = null!;

    [Required]
    public int BookingStatusId { get; set; }
    public BookingStatusEntity BookingStatus { get; set; } = null!;

    public ICollection<PaymentsEntity> Payments { get; set; } = [];
}
