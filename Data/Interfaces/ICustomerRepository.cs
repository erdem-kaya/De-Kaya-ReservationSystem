using Data.Entities;

namespace Data.Interfaces;

public interface ICustomerRepository : IBaseRepository<CustomersEntity>
{
    //Belli bir müşterinin rezervasyonlarını getirir.
    //Get bookings of a specific customer.
    Task<CustomersEntity?> GetCustomerBookingsAsync(int customerId);
}
