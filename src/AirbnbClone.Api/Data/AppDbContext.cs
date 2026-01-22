using AirbnbClone.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace AirbnbClone.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

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
    }
}
