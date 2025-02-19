using Business.Factories;
using Business.Interfaces;
using Business.Models.Payment;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services;

public class PaymentService(IPaymentRepository paymentRepository) : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository = paymentRepository;

    public async Task<PaymentForm> CreateAsync(PaymentRegistrationForm form)
    {
        if (form == null)
            throw new ArgumentNullException(nameof(form), "Payment form cannot be null.");

        await _paymentRepository.BeginTransactionAsync();

        try
        {
            var paymentEntity = PaymentFactory.Create(form);
            var createdPayment = await _paymentRepository.CreateAsync(paymentEntity);
            await _paymentRepository.CommitTransactionAsync();
            return createdPayment != null ? PaymentFactory.Create(createdPayment) : null!;
        }
        catch (Exception ex) 
        {
            await _paymentRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating payment: {ex.Message}");
            return null!;
        }
    }

    public async Task<IEnumerable<PaymentForm>> GetAllAsync()
    {
        try
        {
            var allPayments = await _paymentRepository.GetAllAsync();
            var result = allPayments.Select(PaymentFactory.Create).ToList();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting all payments: {ex.Message}");
            return null!;
        }
    }

    public async Task<PaymentForm> GetByIdAsync(int id)
    {
        try
        {
            var getPaymentWithId = await _paymentRepository.GetAsync(p => p.Id == id);
            var result = getPaymentWithId != null ? PaymentFactory.Create(getPaymentWithId) : null!;
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting payment by id: {ex.Message}");
            return null!;
        }
    }

    public async Task<PaymentForm> UpdateAsync(PaymentUpdateForm form)
    {
        if (form == null)
            return null!;

        try
        {
            var findPayment = await _paymentRepository.GetAsync(p => p.Id == form.Id) ?? throw new Exception($"Payment with ID {form.Id} does not exist.");
            PaymentFactory.Update(findPayment, form);
            var updatedPayment = await _paymentRepository.UpdateAsync(findPayment.Id, findPayment);
            return updatedPayment != null ? PaymentFactory.Create(updatedPayment) : null!;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating payment: {ex.Message}");
            return null!;
        }
    }
    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var findPayment = await _paymentRepository.GetAsync(p => p.Id == id) ?? throw new Exception($"Payment with ID {id} does not exist.");
            var deletedPayment = await _paymentRepository.DeleteAsyncById(id);
            if (!deletedPayment)
                throw new Exception($"Error deleting payment with ID {id}");
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting payment: {ex.Message}");
            return false;
        }
    }

    public async Task<IEnumerable<PaymentForm>> GetPaymentsByBookingIdAsync(int bookingId)
    {
        try
        {
            var payments = await _paymentRepository.GetPaymentsByBookingIdAsync(bookingId);
            var result = payments.Select(PaymentFactory.Create).ToList();
            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting payments by booking ID {bookingId} : {ex.Message}");
            return [];
        }
    }

    public async Task<IEnumerable<PaymentSummary>> GetPaymentSummariesAsync()
    {
        try
        {
            var payments = await _paymentRepository.GetPaymentSummariesAsync();
            var result = payments.Select(PaymentFactory.CreateSummary).ToList();

            return result;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting payment summaries: {ex.Message}");
            return [];
        }
    }

    public async Task<decimal> GetTotalRevenueAsync()
    {
        try
        {
            var totalRevenue = await _paymentRepository.GetTotalRevenueAsync();
            return totalRevenue;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error getting total revenue : {ex.Message}");
            return 0;
        }
    }
}
