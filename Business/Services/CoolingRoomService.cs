using Business.Factories;
using Business.Interfaces;
using Business.Models.CoolingRoom;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public class CoolingRoomService(ICoolingRoomRepository coolingRoomRepository) : ICoolingRoomService
{
    private readonly ICoolingRoomRepository _coolingRoomRepository = coolingRoomRepository;

    public async Task<CoolingRoomForm> CreateAsync(CoolingRoomRegistrationForm form)
    {
        if (form == null) throw new ArgumentNullException(nameof(form), "Cooling Room form cannot be null.");

        await _coolingRoomRepository.BeginTransactionAsync();

        try
        {
            var coolingRoomEntity = CoolingRoomFactory.Create(form);
            var coolingRoom = await _coolingRoomRepository.CreateAsync(coolingRoomEntity);
            await _coolingRoomRepository.CommitTransactionAsync();

            if (coolingRoom == null)
                throw new Exception("Failed to create cooling room.");

            return CoolingRoomFactory.Create(coolingRoom);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating cooling room: {ex.Message}");
            await _coolingRoomRepository.RollbackTransactionAsync();
            return null!;
        }
    }

    public async Task<IEnumerable<CoolingRoomForm>> GetAllAsync()
    {
        try
        {
            var coolingRooms = await _coolingRoomRepository.GetAllAsync();
            return coolingRooms?.Select(CoolingRoomFactory.Create).ToList() ?? [];
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting cooling rooms: {ex.Message}");
            return [];
        }
    }

    public async Task<CoolingRoomForm> GetByIdAsync(int id)
    {
        try
        {
            var coolingRoom = await _coolingRoomRepository.GetAsync(c => c.Id == id);
            return coolingRoom != null ? CoolingRoomFactory.Create(coolingRoom) : null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting cooling room {id}: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var existingCoolingRoom = await _coolingRoomRepository.GetAsync(c => c.Id == id)
                ?? throw new ArgumentNullException(nameof(id), "Cooling Room not found");

            var deleted = await _coolingRoomRepository.DeleteAsyncById(id);
            return deleted;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting cooling room {id}: {ex.Message}");
            return false;
        }
    }

    public async Task<CoolingRoomForm> UpdateAsync(CoolingRoomUpdateForm form)
    {
        if (form == null)
            throw new ArgumentNullException(nameof(form), "Cooling Room update form cannot be null.");

        try
        {
            var findUpdateCoolingRoom = await _coolingRoomRepository.GetAsync(c => c.Id == form.Id)
                ?? throw new Exception($"Cooling Room with ID {form.Id} not found.");

            CoolingRoomFactory.Update(findUpdateCoolingRoom, form);
            var updatedCoolingRoom = await _coolingRoomRepository.UpdateAsync(findUpdateCoolingRoom.Id, findUpdateCoolingRoom);
            return updatedCoolingRoom != null ? CoolingRoomFactory.Create(updatedCoolingRoom) : null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating cooling room {form.Id}: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> UpdateAllCoolingRoomPricesAsync(decimal newPrice)
    {
        try
        {
            var updated = await _coolingRoomRepository.UpdateAllCoolingRoomPricesAsync(newPrice);
            if (!updated)
                throw new Exception("Failed to update cooling room prices.");
            return updated;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating all cooling room prices: {ex.Message}");
            return false;
        }
    }
}
