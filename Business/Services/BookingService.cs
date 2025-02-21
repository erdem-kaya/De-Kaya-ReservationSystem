using Business.Factories;
using Business.Interfaces;
using Business.Models.Booking;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public class BookingService(IBookingRepository bookingRepository) : IBookingService
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;

    public async Task<BookingForm> CreateAsync(BookingRegistrationForm form)
    {
        if (form == null) 
            throw new ArgumentNullException(nameof(form), "Booking form cannot be null.");

        await _bookingRepository.BeginTransactionAsync();

        try
        {
            var bookingEntity = BookingFactory.Create(form);
            var createdBooking = await _bookingRepository.CreateAsync(bookingEntity);
            await _bookingRepository.CommitTransactionAsync();
            return createdBooking != null ? BookingFactory.Create(createdBooking) : null!;
        }
        catch (Exception ex)
        {
            await _bookingRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating booking: {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<BookingForm>> GetAllAsync()
    {
        try
        {
            var allBookings = await _bookingRepository.GetAllAsync();
            var result = allBookings?.Select(BookingFactory.Create).ToList() ?? [];
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all bookings: {ex.Message}");
            return [];
        }
    }

    public async Task<BookingForm> GetByIdAsync(int id)
    {
        try
        {
            var booking = await _bookingRepository.GetAsync(c => c.Id == id);
            var result = booking != null ? BookingFactory.Create(booking) : null!;
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting booking by id: {ex.Message}");
            return null!;
        }
    }

    public async Task<BookingForm> UpdateAsync(BookingUpdateForm form)
    {
        if (form == null)
            throw new ArgumentNullException(nameof(form), "Booking form cannot be null.");

        try
        {
            var findUpdatedBooking = await _bookingRepository.GetAsync(c => c.Id == form.Id)
                ?? throw new Exception($"Booking with ID {form.Id} not found.");

            BookingFactory.Update(findUpdatedBooking, form);
            var updatedBooking = await _bookingRepository.UpdateAsync(findUpdatedBooking.Id, findUpdatedBooking);
            return updatedBooking != null ? BookingFactory.Create(updatedBooking) : null!;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating booking: {ex.Message}");
            return null!;
        }
    }


    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var existingBooking = await _bookingRepository.GetAsync(c => c.Id == id)
                ?? throw new ArgumentNullException(nameof(id), "Booking not found");

            var deleted = await _bookingRepository.DeleteAsyncById(id);
            if (!deleted)
                throw new Exception($"Error deleting booking {id}");
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting booking {id}: {ex.Message}");
            return false;
        }
    }

    public async Task<IEnumerable<BookingForm>> GetBookingsBetweenDateAsync(DateTime startDate, DateTime endDate)
    {
        try
        {
            var bookings = await _bookingRepository.GetBookingsBetweenDateAsync(startDate, endDate);
            var result = bookings?.Select(BookingFactory.Create).ToList() ?? [];
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting bookings between dates: {ex.Message}");
            return [];
        }
    }

    public async Task<IEnumerable<BookingForm>> GetBookingsByPaymentStatusAsync(int paymentStatusId)
    {
        try
        {
            var bookings = await _bookingRepository.GetBookingsByPaymentStatusAsync(paymentStatusId);
            var result = bookings?.Select(BookingFactory.Create).ToList() ?? [];
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting bookings by payment status: {ex.Message}");
            return [];
        }
    }

    public async Task<IEnumerable<BookingSummary>> GetBookingSummariesAsync()
    {
        try
        {
            var bookings = await _bookingRepository.GetBookingSummariesAsync();
            var result = bookings?.Select(BookingFactory.CreateSummary).ToList() ?? [];
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting booking summaries: {ex.Message}");
            return [];
        }
    }

    public async Task<IEnumerable<BookingForm>> GetByCheckInDateAsync(DateTime checkInDate)
    {
        try
        {
            var bookings = await _bookingRepository.GetByCheckInDateAsync(checkInDate);
            var result = bookings?.Select(BookingFactory.Create).ToList() ?? [];
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting bookings by check in date: {ex.Message}");
            return [];
        }
    }

    public async Task<IEnumerable<BookingForm>> GetByCheckOutDateAsync(DateTime checkOutDate)
    {
        try
        {
            var bookings = await _bookingRepository.GetByCheckOutDateAsync(checkOutDate);
            var result = bookings?.Select(BookingFactory.Create).ToList() ?? [];
            return result;

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting bookings by check out date: {ex.Message}");
            return []!;
        }
    }
}
