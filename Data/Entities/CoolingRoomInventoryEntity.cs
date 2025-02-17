using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class CoolingRoomInventoryEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string RoomName { get; set; } = null!;
    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int RoomStatusId { get; set; }
    public CoolingRoomStatusEntity RoomStatus { get; set; } = null!;

    public int CoolingRoomId { get; set; }
    public CoolingRoomEntity CoolingRoom { get; set; } = null!;
}
