using Business.Factories;
using Business.Interfaces;
using Business.Models.Customer;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<CustomerForm> CreateAsync(CustomerRegistrationForm form)
    {
        if (form == null)
            throw new ArgumentNullException(nameof(form), "Customer form cannot be null.");
        
        await _customerRepository.BeginTransactionAsync();

        try
        {
            
            var customerEntity = CustomerFactory.Create(form);
            var createdCustomer = await _customerRepository.CreateAsync(customerEntity);
            await _customerRepository.CommitTransactionAsync();
            return createdCustomer != null ? CustomerFactory.Create(createdCustomer) : null!;

        }
        catch (Exception ex) 
        {
            await _customerRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating customer: {ex.Message}");
            return null!;
        }
    }


    public async Task<IEnumerable<CustomerForm>> GetAllAsync()
    {
        try
        {
            var allCustomers = await _customerRepository.GetAllAsync();
            var result = allCustomers.Select(CustomerFactory.Create).ToList();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all customers: {ex.Message}");
            return [];
        }
    }

    public async Task<CustomerForm> GetByIdAsync(int id)
    {
        try
        {
            var getCustomerWithId = await _customerRepository.GetAsync(c => c.Id == id);
            var result = getCustomerWithId != null ? CustomerFactory.Create(getCustomerWithId) : null!;
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting customer by id: {ex.Message}");
            return null!;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var existingCustomer = await _customerRepository.GetAsync(c => c.Id == id) ?? throw new Exception($"Company with ID {id} does not exist.");

            var deletedCustomer = await _customerRepository.DeleteAsyncById(id);
            if (!deletedCustomer)
                throw new Exception($"Error deleting customer with ID {id}.");
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting customer: {ex.Message}");
            return false;
        }
    }

    public async Task<CustomerForm> UpdateAsync(CustomerUpdateForm form)
    {
        if (form == null)
            return null!;

        try
        {
            var findUpdateCustomer = await _customerRepository.GetAsync(c => c.Id == form.Id)
                                   ?? throw new Exception($"Customer with ID {form.Id} does not exist.");

            CustomerFactory.Update(findUpdateCustomer, form);
            var updatedCustomer = await _customerRepository.UpdateAsync(findUpdateCustomer.Id, findUpdateCustomer);
            return updatedCustomer != null ? CustomerFactory.Create(updatedCustomer) : null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating customer: {ex.Message}");
            return null!;
        }
    }

    public async Task<CustomerSummary?> GetCustomerBookingsAsync(int customerId)
    {
        try
        {
            var customers = await _customerRepository.GetCustomerBookingsAsync(customerId);
            return customers != null ? CustomerFactory.CreateSummary(customers) : null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting bookings of customer {customerId} : {ex.Message}");
            return null!;
        }
    }

   
}
