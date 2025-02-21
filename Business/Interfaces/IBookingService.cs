using Business.Models.Booking;

namespace Business.Interfaces;

public interface IBookingService
{
    Task<BookingForm> CreateAsync(BookingRegistrationForm form);
    Task<IEnumerable<BookingForm>> GetAllAsync();
    Task<BookingForm> GetByIdAsync(int id);
    Task<BookingForm> UpdateAsync(BookingUpdateForm form);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<BookingSummary>> GetBookingSummariesAsync();
    Task<IEnumerable<BookingForm>> GetByCheckInDateAsync(DateTime checkInDate);
    Task<IEnumerable<BookingForm>> GetByCheckOutDateAsync(DateTime checkOutDate);
    Task<IEnumerable<BookingForm>> GetBookingsBetweenDateAsync(DateTime startDate, DateTime endDate);
    Task<IEnumerable<BookingForm>> GetBookingsByPaymentStatusAsync(int paymentStatusId);
}
