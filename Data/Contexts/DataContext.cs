using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<BookingsEntity> Bookings { get; set; } = null!;
    public DbSet<BookingStatusEntity> BookingStatuses { get; set; } = null!;
    public DbSet<CoolingRoomEntity> CoolingRooms { get; set; } = null!;
    public DbSet<CoolingRoomInventoryEntity> CoolingRoomInventories { get; set; } = null!;
    public DbSet<CoolingRoomStatusEntity> CoolingRoomStatuses { get; set; } = null!;
    public DbSet<CustomersEntity> Customers { get; set; } = null!;
    public DbSet<PaymentsEntity> Payments { get; set; } = null!;
    public DbSet<PaymentStatusEntity> PaymentStatuses { get; set; } = null!;
    public DbSet<RoleEntity> Roles { get; set; } = null!;
    public DbSet<UsersEntity> Users { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Kullanıcı - Rol ilişkisi
        modelBuilder.Entity<UsersEntity>()
            .HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict); // Kullanıcı silindiğinde rolü silme

        // Kullanıcı - Müşteri ilişkisi
        modelBuilder.Entity<CustomersEntity>()
            .HasOne(c => c.User)
            .WithMany(u => u.Customers)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Kullanıcı silinirse müşterileri de silinsin

        // Rezervasyon - Müşteri ilişkisi
        modelBuilder.Entity<BookingsEntity>()
            .HasOne(b => b.Customer)
            .WithMany(c => c.Bookings)
            .HasForeignKey(b => b.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // Rezervasyon - Soğuk hava odası ilişkisi
        modelBuilder.Entity<BookingsEntity>()
            .HasOne(b => b.CoolingRoom)
            .WithMany()
            .HasForeignKey(b => b.CoolingRoomId)
            .OnDelete(DeleteBehavior.Cascade);

        // Rezervasyon - Ödeme ilişkisi
        modelBuilder.Entity<PaymentsEntity>()
            .HasOne(p => p.Booking)
            .WithMany(b => b.Payments)
            .HasForeignKey(p => p.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        // CoolingRoomInventory - CoolingRoom ilişkisi
        modelBuilder.Entity<CoolingRoomInventoryEntity>()
            .HasOne(ci => ci.CoolingRoom)
            .WithMany(cr => cr.CoolingRoomInventories)
            .HasForeignKey(ci => ci.CoolingRoomId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }

}
