using Business.Interfaces;
using Business.Models.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController(IPaymentService paymentService) : ControllerBase
    {
        private readonly IPaymentService _paymentService = paymentService;

        [HttpPost]
        public async Task<IActionResult> CreatePaymentAsync(PaymentRegistrationForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var createdPayment = await _paymentService.CreateAsync(form);
            var result = createdPayment != null ? Ok(createdPayment) : Problem("Payment could not be created.");
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentsAsync()
        {
            var allPayments = await _paymentService.GetAllAsync();
            if (allPayments != null)
                return Ok(allPayments);
            return NotFound("There are no payments to view.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentByIdAsync(int id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            if (payment != null)
                return Ok(payment);
            return NotFound($"Payment with id {id} not found.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentAsync(int id, PaymentUpdateForm form)
        {
            if (ModelState.IsValid)
            {
                var payment = await _paymentService.GetByIdAsync(id);
                if (payment == null)
                    return NotFound($"Payment with id {id} not found.");
                var updatedPayment = await _paymentService.UpdateAsync(form);
                return updatedPayment != null ? Ok(updatedPayment) : Problem("Payment could not be updated.");
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentAsync(int id)
        {
            var deleted = await _paymentService.DeleteAsync(id);
            return deleted ? Ok() : NotFound($"Payment with id {id} not found.");
        }

        [HttpGet("revenue")]
        public async Task<IActionResult> GetTotalRevenueAsync()
        {
            var totalRevenue = await _paymentService.GetTotalRevenueAsync();
            return Ok(totalRevenue);
        }

        [HttpGet("{bookingId}/payments")]
        public async Task<IActionResult> GetPaymentsByBookingIdAsync(int bookingId)
        {
            var payments = await _paymentService.GetPaymentsByBookingIdAsync(bookingId);
            if (payments != null)
                return Ok(payments);
            return NotFound($"No payments found for booking with id {bookingId}.");
        }

        [HttpGet("summaries")]
        public async Task<IActionResult> GetPaymentSummariesAsync()
        {
            var paymentSummaries = await _paymentService.GetPaymentSummariesAsync();
            if (paymentSummaries != null)
                return Ok(paymentSummaries);
            return NotFound("No payment summaries found.");
        }
    }
}
