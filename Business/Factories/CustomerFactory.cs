using Business.Models.Customer;
using Data.Entities;

namespace Business.Factories;

public class CustomerFactory
{
    public static CustomersEntity Create(CustomerRegistrationForm form) => new()
    {
        FirstName = form.FirstName,
        LastName = form.LastName,
        Address = form.Address,
        City = form.City,
        PhoneNumber = form.PhoneNumber,
        UserId = form.UserId,
        CreatedAt = DateTime.UtcNow
    };

    public static CustomerForm Create(CustomersEntity entity) => new()
    {
        Id = entity.Id,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        Address = entity.Address,
        City = entity.City,
        PhoneNumber = entity.PhoneNumber,
        UserId = entity.UserId
    };

    public static void Update(CustomersEntity entity, CustomerUpdateForm form)
    {
        if (!string.IsNullOrEmpty(form.FirstName))
            entity.FirstName = form.FirstName;

        if (!string.IsNullOrEmpty(form.LastName))
            entity.LastName = form.LastName;

        if (!string.IsNullOrEmpty(form.Address))
            entity.Address = form.Address;

        if (!string.IsNullOrEmpty(form.City))
            entity.City = form.City;

        if (!string.IsNullOrEmpty(form.PhoneNumber))
            entity.PhoneNumber = form.PhoneNumber;

        if (form.UserId.HasValue)
            entity.UserId = form.UserId.Value;
    }

    public static CustomerSummary CreateSummary(CustomersEntity entity) => new()
    {
        Id = entity.Id,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        Address = entity.Address,
        City = entity.City,
        PhoneNumber = entity.PhoneNumber,
        CreatedAt = entity.CreatedAt,
        BookingCount = entity.Bookings.Count
    };
}
