using Data.Entities;

namespace Data.Interfaces;

public interface IBookingRepository : IBaseRepository<BookingsEntity>
{
    // Toplam rezervasyonları getirir.
    // Get all bookings.
    Task<IEnumerable<BookingsEntity>> GetBookingSummariesAsync();
    // CheckIn tarihine göre rezervasyonları getirir.
    // Get bookings by check-in date.
    Task<IEnumerable<BookingsEntity>> GetByCheckInDateAsync(DateTime checkInDate);

    // CheckOut tarihine göre rezervasyonları getirir.
    // Get bookings by check-out date.
    Task<IEnumerable<BookingsEntity>> GetByCheckOutDateAsync(DateTime checkOutDate);

    // Belli bir tarih aralığındaki rezervasyonları getirir.
    // Get bookings between a certain date range.
    Task<IEnumerable<BookingsEntity>> GetBookingsBetweenDateAsync(DateTime startDate, DateTime endDate);

    // Ödeme durumuna göre rezervasyonları getirir.
    // Get bookings by payment status.
    Task<IEnumerable<BookingsEntity>> GetBookingsByPaymentStatusAsync(int paymentStatusId);


}