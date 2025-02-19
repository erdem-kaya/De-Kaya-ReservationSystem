using Business.Models.Payment;
using Data.Entities;

namespace Business.Factories;

public class PaymentFactory
{
    public static PaymentsEntity Create(PaymentRegistrationForm form) => new()
    {
        PaymentDate = form.PaymentDate,
        Amount = form.Amount,
        PaymentMethod = form.PaymentMethod,
        PrePayment = form.PrePayment ?? 0,
        ConfirmedAt = form.ConfirmedAt,
        PaymentStatusId = form.PaymentStatusId,
        BookingId = form.BookingId
    };

    public static PaymentForm Create(PaymentsEntity entity) => new()
    {
        Id = entity.Id,
        PaymentDate = entity.PaymentDate,
        Amount = entity.Amount,
        PaymentMethod = entity.PaymentMethod,
        PrePayment = entity.PrePayment,
        ConfirmedAt = entity.ConfirmedAt,
        PaymentStatusId = entity.PaymentStatusId,
        BookingId = entity.BookingId
    };

    public static PaymentsEntity Update(PaymentsEntity entity, PaymentUpdateForm form)
    {
        
        entity.PaymentDate = form.PaymentDate;
        entity.Amount = form.Amount;
        entity.PaymentMethod = form.PaymentMethod;

        if (form.PrePayment.HasValue)
            entity.PrePayment = form.PrePayment;

        if (form.ConfirmedAt.HasValue)
            entity.ConfirmedAt = form.ConfirmedAt;

        entity.PaymentStatusId = form.PaymentStatusId;
        entity.BookingId = form.BookingId;

        return entity;
    }

    public static PaymentSummary CreateSummary(PaymentsEntity entity) => new()
    {
        Id = entity.Id,
        PaymentDate = entity.PaymentDate,
        Amount = entity.Amount,
        PaymentMethod = entity.PaymentMethod,
        PrePayment = entity.PrePayment,
        ConfirmedAt = entity.ConfirmedAt,
        PaymentStatusId = entity.PaymentStatusId,
        BookingId = entity.BookingId

    };
}
