using Data.Entities;

namespace Data.Interfaces;

public interface ICoolingRoomRepository : IBaseRepository<CoolingRoomEntity>
{
    // Kullanılabilir soğutma odalarını getirir.
    // Get available cooling rooms.
    Task<IEnumerable<CoolingRoomEntity>> GetAvailableCoolingRoomsAsync();

    // Belli tarih aralığında müsait oda sayısı.
    // Number of available rooms in a certain date range.
    Task<int> GetAvailableCoolingRoomsCountAsync(DateTime startDate, DateTime endDate);
}
