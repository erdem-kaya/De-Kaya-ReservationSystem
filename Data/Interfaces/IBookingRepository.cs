using Data.Entities;

namespace Data.Interfaces;

public interface IBookingRepository : IBaseRepository<BookingsEntity>
{
    // CheckIn tarihine göre rezervasyonları getirir.
    // Get bookings by check-in date.
    Task<IEnumerable<BookingsEntity>> GetByCheckInDateAsync(DateTime checkInDate);

    // Belli bir tarih aralığındaki rezervasyonları getirir.
    // Get bookings between a certain date range.
    Task<IEnumerable<BookingsEntity>> GetBookingsBetweenDateAsync(DateTime startDate, DateTime endDate);
}