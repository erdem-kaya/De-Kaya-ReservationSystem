namespace Business.Models.CoolingRoomInventory;

public class CoolingRoomInventoryForm
{
    public int Id { get; set; }
    public string RoomName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int RoomStatusId { get; set; }
    public int CoolingRoomId { get; set; }
}
