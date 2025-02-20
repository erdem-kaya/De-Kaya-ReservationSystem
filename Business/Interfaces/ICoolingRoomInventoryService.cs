using Business.Models.CoolingRoomInventory;

namespace Business.Interfaces;

public interface ICoolingRoomInventoryService
{
    Task<CoolingRoomInventoryForm> CreateAsync(CoolingRoomInventoryRegistrationForm form);
    Task<IEnumerable<CoolingRoomInventoryForm>> GetAllAsync();
    Task<CoolingRoomInventoryForm> GetByIdAsync(int id);
    Task<CoolingRoomInventoryForm> UpdateAsync(CoolingRoomInventoryUpdateForm form);
    Task<bool> DeleteAsync(int id);
}
