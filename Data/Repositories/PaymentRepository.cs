using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;

public class PaymentRepository(DataContext context) : BaseRepository<PaymentsEntity>(context), IPaymentRepository
{
    public async Task<IEnumerable<PaymentsEntity>> GetPaymentsByBookingIdAsync(int bookingId)
    {
        try
        {
            var payments = await _context.Payments
                .Where(p => p.BookingId == bookingId)
                .ToListAsync();
            return payments;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting payments by booking ID {bookingId} : {ex.Message}");
            return [];
        }
    }

    public async Task<decimal> GetTotalRevenueAsync()
    {
        try
        {
            var totalRevenue = await _context.Payments.SumAsync(p => p.Amount);
            return totalRevenue;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting total revenue : {ex.Message}");
            return 0;
        }
    }

    public async Task<IEnumerable<PaymentsEntity>> GetPaymentSummariesAsync()
    {
        try
        {
            var payments = await _context.Payments.ToListAsync();
            return payments;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting payment summaries: {ex.Message}");
            return [];
        }
    }
}
