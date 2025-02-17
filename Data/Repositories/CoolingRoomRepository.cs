using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;

public class CoolingRoomRepository(DataContext context) : BaseRepository<CoolingRoomEntity>(context), ICoolingRoomRepository
{
    public async Task<IEnumerable<CoolingRoomEntity>> GetAvailableCoolingRoomsAsync()
    {
       try
       {
            var availableCoolingRooms = await _context.CoolingRooms
                .Include(cr => cr.CoolingRoomInventories)
                .Where(x => x.CoolingRoomInventories.Any(y => y.RoomStatusId == 1))
                .ToListAsync();
            return availableCoolingRooms;
        }
       catch (Exception ex)
       {
         Debug.WriteLine($"Error getting available cooling rooms : {ex.Message}");
         return null!;
       }
    }

    public async Task<int> GetAvailableCoolingRoomsCountAsync(DateTime startDate, DateTime endDate)
    {
        try
        {
            var bookedRoomIds = await _context.Bookings
                        .Where(b => b.CheckInDate < endDate && b.CheckOutDate > startDate)
                        .Select(b => b.CoolingRoomId)
                        .Distinct()
                        .ToListAsync();

            int availableCoolingRoomsCount = await _context.CoolingRooms
                .CountAsync(cr => !bookedRoomIds.Contains(cr.Id));

            return availableCoolingRoomsCount;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting available cooling rooms count : {ex.Message}");
            return 0;
        }
    }
}
