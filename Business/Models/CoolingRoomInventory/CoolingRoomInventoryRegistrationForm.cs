namespace Business.Models.CoolingRoomInventory;

public class CoolingRoomInventoryRegistrationForm
{
    public string RoomName { get; set; } = null!;
    public int RoomStatusId { get; set; }
    public int CoolingRoomId { get; set; }
}
