using Data.Entities;

namespace Data.Interfaces;

public interface IPaymentRepository : IBaseRepository<PaymentsEntity>
{
    // Toplam geliri getirir.
    // Get total revenue.
    Task<decimal> GetTotalRevenueAsync();

    // Rezervasyon ID'sine göre ödemeleri getirir.
    // Get payments by booking ID.
    Task<IEnumerable<PaymentsEntity>> GetPaymentsByBookingIdAsync(int bookingId);

    // Ödeme özetlerini getirir.
    // Get payment summaries.
    Task<IEnumerable<PaymentsEntity>> GetPaymentSummariesAsync();


}
