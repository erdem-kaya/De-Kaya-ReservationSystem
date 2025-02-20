using Business.Models.CoolingRoomInventory;
using Data.Entities;

namespace Business.Factories;

public class CoolingRoomInventoryFactory
{
    public static CoolingRoomInventoryEntity Create(CoolingRoomInventoryRegistrationForm form) => new()
    {
        RoomName = form.RoomName,
        CreatedAt = DateTime.UtcNow,
        RoomStatusId = form.RoomStatusId,
        CoolingRoomId = form.CoolingRoomId
    };

    public static CoolingRoomInventoryForm Create(CoolingRoomInventoryEntity entity) => new()
    {
        Id = entity.Id,
        RoomName = entity.RoomName,
        CreatedAt = entity.CreatedAt,
        RoomStatusId = entity.RoomStatusId,
        CoolingRoomId = entity.CoolingRoomId
    };

    public static void Update(CoolingRoomInventoryEntity entity, CoolingRoomInventoryUpdateForm form)
    {
        if (!string.IsNullOrEmpty(form.RoomName))
            entity.RoomName = form.RoomName;
        if (form.RoomStatusId.HasValue)
            entity.RoomStatusId = form.RoomStatusId.Value;
        if (form.CoolingRoomId.HasValue)
            entity.CoolingRoomId = form.CoolingRoomId.Value;
    }


}
