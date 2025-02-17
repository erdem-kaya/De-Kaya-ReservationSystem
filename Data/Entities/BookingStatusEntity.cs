using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class BookingStatusEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string StatusName { get; set; } = null!;
}


