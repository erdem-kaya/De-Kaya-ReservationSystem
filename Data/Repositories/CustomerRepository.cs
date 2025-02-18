using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : BaseRepository<CustomersEntity>(context), ICustomerRepository
{
    public async Task<CustomersEntity?> GetCustomerBookingsAsync(int customerId)
    {
       try
       {
            var customers = await _context.Customers
                .Where(c => c.Id == customerId)
                .Include(c => c.Bookings)
                    .ThenInclude(b => b.CoolingRoom)
                .Include(c => c.Bookings)
                    .ThenInclude(p => p.Payments)
                .FirstOrDefaultAsync();
            
            return customers;
        }
       catch (Exception ex)
       {
            Debug.WriteLine($"Error getting bookings of customer {customerId} : {ex.Message}");
            return null;
       }
    }
}
