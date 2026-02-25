using AirbnbClone.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace AirbnbClone.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Listing> Listings => Set<Listing>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(b =>
        {
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.Email).IsUnique();

            b.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(320);

            b.Property(x => x.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

            b.Property(x => x.CreatedAtUtc)
                .IsRequired();
        });

        modelBuilder.Entity<Listing>(b =>
        {
            b.ToTable("Listings");

            b.HasKey(x => x.Id);

            b.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            b.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(100);

            b.Property(x => x.ImageUrl)
                .IsRequired()
                .HasMaxLength(500);

            b.Property(x => x.PricePerNight)
                .IsRequired();

            b.Property(x => x.MaxGuests)
                .IsRequired();

            b.Property(x => x.CreatedAtUtc)
                .IsRequired();

            b.HasIndex(x => x.City);
            b.HasIndex(x => x.PricePerNight);
        });

        modelBuilder.Entity<Booking>(b =>
        {
            b.ToTable("Bookings");
            b.HasKey(x => x.Id);

            b.Property(x => x.ListingId).IsRequired();
            b.Property(x => x.GuestId).IsRequired();

            b.Property(x => x.CheckIn).IsRequired();
            b.Property(x => x.CheckOut).IsRequired();

            b.Property(x => x.Status).IsRequired();
            b.Property(x => x.CreatedAtUtc).IsRequired();

            b.HasIndex(x => x.ListingId);
            b.HasIndex(x => x.GuestId);
            b.HasIndex(x => new { x.ListingId, x.CheckIn, x.CheckOut });
        });
    }
}
