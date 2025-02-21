using Business.Interfaces;
using Business.Models.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        private readonly ICustomerService _customerService = customerService;

        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync(CustomerRegistrationForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var createdCustomer = await _customerService.CreateAsync(form);
            var result = createdCustomer != null ? Ok(createdCustomer) : Problem("Customer could not be created.");
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomersAsync()
        {
            var allCustomers = await _customerService.GetAllAsync();
            if (allCustomers != null)
                return Ok(allCustomers);
            return NotFound("There are no customers to view.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer != null)
                return Ok(customer);
            return NotFound($"Customer with id {id} not found.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerAsync(int id, CustomerUpdateForm form)
        {
            if (ModelState.IsValid)
            {
                var customer = await _customerService.GetByIdAsync(id);
                if (customer == null)
                    return NotFound($"Customer with id {id} not found.");
                var updatedCustomer = await _customerService.UpdateAsync(form);
                return updatedCustomer != null ? Ok(updatedCustomer) : Problem("Customer could not be updated.");
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
            var deleted = await _customerService.DeleteAsync(id);
            return deleted ? Ok() : NotFound($"Customer with id {id} not found.");
        }

        [HttpGet("{id}/customer-bookings")]
        public async Task<IActionResult> GetCustomerBookingsAsync(int id)
        {
            var customerBookings = await _customerService.GetCustomerBookingsAsync(id);
            if (customerBookings != null)
                return Ok(customerBookings);
            return NotFound($"Customer with id {id} has no bookings.");
        }
    }
}
