using Business.Factories;
using Business.Interfaces;
using Business.Models.CoolingRoomInventory;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public class CoolingRoomInventoryService(ICoolingRoomInventoryRepository coolingRoomInventoryRepository) : ICoolingRoomInventoryService
{
    private readonly ICoolingRoomInventoryRepository _coolingRoomInventoryRepository = coolingRoomInventoryRepository;

    public async Task<CoolingRoomInventoryForm> CreateAsync(CoolingRoomInventoryRegistrationForm form)
    {
        if (form == null) throw new ArgumentNullException(nameof(form), "Cooling Room form cannot be null.");

        await _coolingRoomInventoryRepository.BeginTransactionAsync();

        try
        {
            var coolingRoomInventoryEntity = CoolingRoomInventoryFactory.Create(form);
            var coolingRoomInventory = await _coolingRoomInventoryRepository.CreateAsync(coolingRoomInventoryEntity);
            await _coolingRoomInventoryRepository.CommitTransactionAsync();

            if (coolingRoomInventory == null)
                throw new Exception("Failed to create cooling room inventory.");
            return CoolingRoomInventoryFactory.Create(coolingRoomInventory);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating cooling room inventory: {ex.Message}");
            await _coolingRoomInventoryRepository.RollbackTransactionAsync();
            return null!;
        }
    }

    public async Task<IEnumerable<CoolingRoomInventoryForm>> GetAllAsync()
    {
        try
        {
            var coolingRoomInventories = await _coolingRoomInventoryRepository.GetAllAsync();
            return coolingRoomInventories?.Select(CoolingRoomInventoryFactory.Create).ToList() ?? [];
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting cooling room inventories: {ex.Message}");
            return [];
        }
    }

    public async Task<CoolingRoomInventoryForm> GetByIdAsync(int id)
    {
        try
        {
            var existingCoolingRoomInventory = await _coolingRoomInventoryRepository.GetAsync(c => c.Id == id);
            return existingCoolingRoomInventory != null ? CoolingRoomInventoryFactory.Create(existingCoolingRoomInventory) : null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting cooling room inventory {id}: {ex.Message}");
            return null!;
        }
    }

    public async Task<CoolingRoomInventoryForm> UpdateAsync(CoolingRoomInventoryUpdateForm form)
    {
        if (form == null) throw new ArgumentNullException(nameof(form), "Cooling Room form cannot be null.");

        try
        {
            var findCoolingRoomInventory = await _coolingRoomInventoryRepository.GetAsync(c => c.Id == form.Id)
                ?? throw new ArgumentNullException(nameof(form.Id), "Cooling Room Inventory not found");

            CoolingRoomInventoryFactory.Update(findCoolingRoomInventory, form);
            var updatedCoolingRoomInventory = await _coolingRoomInventoryRepository.UpdateAsync(findCoolingRoomInventory.Id, findCoolingRoomInventory);
            return updatedCoolingRoomInventory != null ? CoolingRoomInventoryFactory.Create(updatedCoolingRoomInventory) : null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating cooling room inventory: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var existingCoolingRoomInventory = await _coolingRoomInventoryRepository.GetAsync(c => c.Id == id)
                ?? throw new ArgumentNullException(nameof(id), "Cooling Room Inventory not found");

            var deleted = await _coolingRoomInventoryRepository.DeleteAsyncById(id);
            return existingCoolingRoomInventory != null && deleted; ;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting cooling room inventory {id}: {ex.Message}");
            return false;
        }
    }
}
