using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class PaymentStatusEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string StatusName { get; set; } = null!;
}


