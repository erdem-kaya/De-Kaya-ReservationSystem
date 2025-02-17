using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class CoolingRoomEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,2)" )]
    public decimal UnitPrice { get; set; }

    public ICollection<CoolingRoomInventoryEntity> CoolingRoomInventories { get; set; } = [];
}
