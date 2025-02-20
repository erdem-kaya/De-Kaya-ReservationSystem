namespace Business.Models.CoolingRoomInventory;

public class CoolingRoomInventoryUpdateForm
{
    public int Id { get; set; }
    public string? RoomName { get; set; }
    public int? RoomStatusId { get; set; }
    public int? CoolingRoomId { get; set; }
}
