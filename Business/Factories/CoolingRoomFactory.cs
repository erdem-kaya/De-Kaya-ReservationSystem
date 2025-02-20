using Business.Models.CoolingRoom;
using Data.Entities;

namespace Business.Factories;

public class CoolingRoomFactory
{
    public static CoolingRoomEntity Create(CoolingRoomRegistrationForm form) => new()
    {
        UnitPrice = form.UnitPrice
    };

    public static CoolingRoomForm Create(CoolingRoomEntity entity) => new()
    {
        Id = entity.Id,
        UnitPrice = entity.UnitPrice
    };

    public static void Update(CoolingRoomEntity entity, CoolingRoomUpdateForm form)
    {
        entity.UnitPrice = form.UnitPrice;
    }


    public static CoolingRoomSummary CreateSummary(CoolingRoomEntity entity) => new()
    {
        Id = entity.Id,
        UnitPrice = entity.UnitPrice
    };
}
