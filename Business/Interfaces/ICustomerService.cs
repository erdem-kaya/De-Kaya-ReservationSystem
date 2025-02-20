using Business.Models.Customer;

namespace Business.Interfaces;

public interface ICustomerService
{
    Task<CustomerForm> CreateAsync(CustomerRegistrationForm form);
    Task<IEnumerable<CustomerForm>> GetAllAsync();
    Task<CustomerForm> GetByIdAsync(int id);
    Task<CustomerForm> UpdateAsync(CustomerUpdateForm form);
    Task<bool> DeleteAsync(int id);
    Task<CustomerSummary?> GetCustomerBookingsAsync(int customerId);
}
