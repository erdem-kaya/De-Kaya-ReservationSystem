using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class CoolingRoomStatusEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string StatusName { get; set; } = null!;

    public ICollection<CoolingRoomInventoryEntity> CoolingRoomInventories { get; set; } = [];
}


