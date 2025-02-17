using Data.Entities;

namespace Data.Interfaces;

public interface IBookingRepository : IBaseRepository<BookingsEntity>
{
    // Musterı adına göre rezervasyonları getirir.
    // Get bookings by customer name.
    Task<IEnumerable<BookingsEntity>> GetBookingByCustomerName(string username); 
}
