using Microsoft.EntityFrameworkCore;
using backend.Models;
namespace backend;

public class ApplicationDbContext : DbContext
{
  public DbSet<User> Users { get; set; } = null!;
  public DbSet<City> Cities { get; set; } = null!;
  public DbSet<House> Houses { get; set; } = null!;
  public DbSet<BookingSession> BookingSessions { get; set; } = null!;
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=booking.db");
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.Entity<User>(x =>
    {
      x.HasIndex(x => x.Email).IsUnique(true);
    });

    modelBuilder.Entity<House>(x =>
    {
      x.HasOne(x => x.User).WithMany(x => x.Houses).HasForeignKey(x => x.UserId).IsRequired(true);
      x.HasMany(x => x.BookingSessions).WithOne(x => x.House).HasForeignKey(x => x.HouseId).IsRequired(true);
    });

    modelBuilder.Entity<City>(x =>
  {
    x.HasMany(x => x.Houses).WithOne(x => x.City).HasForeignKey(x => x.CityId).IsRequired(true);
    x.HasData(new City[]{
      new (){Id=1,CityName="Istanbul"},
      new (){Id=2,CityName="Baghdad"},
      new (){Id=3,CityName="Babel"},
      new (){Id=4,CityName="The ottoman empire"},
    });
  });
    modelBuilder.Entity<BookingSession>(x =>
    {
      x.HasOne(x => x.House).WithMany(x => x.BookingSessions).HasForeignKey(x => x.HouseId).IsRequired(true);
      x.HasOne(x => x.User).WithMany(x => x.BookingSessions).HasForeignKey(x => x.UserId).IsRequired(true);

    });

  }

}