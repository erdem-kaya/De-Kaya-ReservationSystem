using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;


public class BookingRepository(DataContext context) : BaseRepository<BookingsEntity>(context), IBookingRepository
{
     public async Task<IEnumerable<BookingsEntity>> GetByCheckInDateAsync(DateTime checkInDate)
    {
        try
        {
            var bookings = await _context.Bookings.Where(x => x.CheckInDate.Date == checkInDate.Date).ToListAsync();
            return bookings;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting bookings by check-in date : {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<BookingsEntity>> GetBookingsBetweenDateAsync(DateTime startDate, DateTime endDate)
    {
        try
        {
            var bookings = await _context.Bookings
                                .Where(x => x.CheckInDate.Date >= startDate.Date && x.CheckInDate.Date <= endDate.Date)
                                .ToListAsync();
            return bookings;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting bookings between dates : {ex.Message}");
            return null!;
        }
    }
}
