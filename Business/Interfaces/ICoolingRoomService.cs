using Business.Models.CoolingRoom;

namespace Business.Interfaces;

public interface ICoolingRoomService
{
    Task<CoolingRoomForm> CreateAsync(CoolingRoomRegistrationForm form);
    Task<IEnumerable<CoolingRoomForm>> GetAllAsync();
    Task<CoolingRoomForm> GetByIdAsync(int id);
    Task<CoolingRoomForm> UpdateAsync(CoolingRoomUpdateForm form);
    Task<bool> DeleteAsync(int id);
    //Tüm fiyatları bir seferde güncellemek için kullanılır.
    //Update all prices at once.
    Task<bool> UpdateAllCoolingRoomPricesAsync(decimal newPrice);
}
