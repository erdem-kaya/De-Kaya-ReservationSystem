using Business.Models.Booking;
using Data.Entities;

namespace Business.Factories;

public class BookingFactory
{
    public static BookingsEntity Create(BookingRegistrationForm form) => new()
    {
        BookingDate = form.BookingDate,
        CheckInDate = form.CheckInDate,
        CheckOutDate = form.CheckOutDate,
        TotalPrice = form.TotalPrice,
        Description = form.Description,
        CustomerId = form.CustomerId,
        CoolingRoomId = form.CoolingRoomId,
        BookingStatusId = form.BookingStatusId,
        CreatedAt = DateTime.UtcNow
    };

    public static BookingForm Create(BookingsEntity entity) => new()
    {
        Id = entity.Id,
        BookingDate = entity.BookingDate,
        CheckInDate = entity.CheckInDate,
        CheckOutDate = entity.CheckOutDate,
        TotalPrice = entity.TotalPrice,
        Description = entity.Description,
        CustomerId = entity.CustomerId,
        CoolingRoomId = entity.CoolingRoomId,
        BookingStatusId = entity.BookingStatusId
    };

    public static void Update(BookingsEntity entity, BookingUpdateForm form)
    {
        entity.CheckInDate = form.CheckInDate;
        entity.CheckOutDate = form.CheckOutDate;
        entity.TotalPrice = form.TotalPrice;
        entity.Description = form.Description;
        entity.BookingStatusId = form.BookingStatusId;
        entity.UpdatedAt = DateTime.UtcNow;
    }

    public static BookingSummary CreateSummary(BookingsEntity entity) => new()
    {
        Id = entity.Id,
        BookingDate = entity.BookingDate,
        CheckInDate = entity.CheckInDate,
        CheckOutDate = entity.CheckOutDate,
        TotalPrice = entity.TotalPrice,
        Description = entity.Description,
        CustomerId = entity.CustomerId,
        CustomerName = $"{entity.Customer.FirstName} {entity.Customer.LastName}",
        CoolingRoomId = entity.CoolingRoomId,
        CoolingRoomName = entity.CoolingRoom.UnitPrice.ToString("C"),
        BookingStatusId = entity.BookingStatusId,
        BookingStatus = entity.BookingStatus.StatusName,
        IsFullyPaid = entity.Payments.Sum(p => p.Amount) >= entity.TotalPrice
    };
}
