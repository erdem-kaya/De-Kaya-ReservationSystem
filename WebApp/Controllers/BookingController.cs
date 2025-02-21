using Business.Interfaces;
using Business.Models.Booking;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController(IBookingService bookingService) : ControllerBase
    {
        private readonly IBookingService _bookingService = bookingService;

        [HttpPost]
        public async Task<IActionResult> CreateBookingAsync(BookingRegistrationForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var createdBooking = await _bookingService.CreateAsync(form);
            var result = createdBooking != null ? Ok(createdBooking) : Problem("Booking could not be created.");
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBookingsAsync()
        {
            var allBookings = await _bookingService.GetAllAsync();
            if (allBookings != null)
                return Ok(allBookings);
            return NotFound("There are no bookings to view.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking != null)
                return Ok(booking);
            return NotFound($"Booking with id {id} not found.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookingAsync(int id, BookingUpdateForm form)
        {
            if (ModelState.IsValid)
            {
                var booking = await _bookingService.GetByIdAsync(id);
                if (booking == null)
                    return NotFound($"Booking with id {id} not found.");
                var updatedBooking = await _bookingService.UpdateAsync(form);
                return updatedBooking != null ? Ok(updatedBooking) : Problem("Booking could not be updated.");
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingAsync(int id)
        {
            var deleted = await _bookingService.DeleteAsync(id);
            return deleted ? Ok() : NotFound($"Booking with id {id} not found.");
        }

        [HttpGet("summaries")]
        public async Task<IActionResult> GetBookingSummariesAsync()
        {
            var summaries = await _bookingService.GetBookingSummariesAsync();
            if (summaries != null)
                return Ok(summaries);
            return NotFound("There are no booking summaries to view.");
        }

        [HttpGet("checkin/{checkInDate}")]
        public async Task<IActionResult> GetBookingsByCheckInDateAsync(DateTime checkInDate)
        {
            var bookings = await _bookingService.GetByCheckInDateAsync(checkInDate);
            if (bookings != null)
                return Ok(bookings);
            return NotFound("There are no bookings with that check-in date.");
        }

        [HttpGet("checkout/{checkOutDate}")]
        public async Task<IActionResult> GetBookingsByCheckOutDateAsync(DateTime checkOutDate)
        {
            var bookings = await _bookingService.GetByCheckOutDateAsync(checkOutDate);
            if (bookings != null)
                return Ok(bookings);
            return NotFound("There are no bookings with that check-out date.");
        }

        [HttpGet("between/{startDate}/{endDate}")]
        public async Task<IActionResult> GetBookingsBetweenDatesAsync(DateTime startDate, DateTime endDate)
        {
            var bookings = await _bookingService.GetBookingsBetweenDateAsync(startDate, endDate);
            if (bookings != null)
                return Ok(bookings);
            return NotFound("There are no bookings between those dates.");
        }

        [HttpGet("payment-status/{paymentStatusId}")]
        public async Task<IActionResult> GetBookingsByPaymentStatusAsync(int paymentStatusId)
        {
            var bookings = await _bookingService.GetBookingsByPaymentStatusAsync(paymentStatusId);
            if (bookings != null)
                return Ok(bookings);
            return NotFound("There are no bookings with that payment status.");
        }
    }
}
