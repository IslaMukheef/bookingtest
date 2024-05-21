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
	private readonly IPasswordHasher _hasher;
	public ApplicationDbContext(IPasswordHasher hasher)
	{
		_hasher = hasher;
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<User>(x =>
		{
			x.HasIndex(x => x.Email).IsUnique(true);
			x.HasData(new User[]
			{
			new User()
			{
				Id = 1,
				Email="Isla@mukheef.com",
				FullName="Isla Mukheef",
				PasswordHash = _hasher.HashPassword("IslaMukheef"),
				PassportNumber="123456",
				IsAdmin=true,
			}
			});
		});

		modelBuilder.Entity<House>(x =>
		{
			x.HasMany(x => x.BookingSessions).WithOne(x => x.House).HasForeignKey(x => x.HouseId).IsRequired(true);
		});

		modelBuilder.Entity<City>(x =>
	  {
	  x.HasMany(x => x.Houses).WithOne(x => x.City).HasForeignKey(x => x.CityId).IsRequired(true);
	  var json = File.ReadAllText("./Cities.Json");

	  var cities = System.Text.Json.JsonSerializer.Deserialize<List<string>>(json);
		x.HasData(cities.Select((x,i) => new City() {Id=i+1, CityName = x }));
	});
		modelBuilder.Entity<BookingSession>(x =>
		{
			x.HasOne(x => x.House).WithMany(x => x.BookingSessions).HasForeignKey(x => x.HouseId).IsRequired(true);
	x.HasOne(x => x.User).WithMany(x => x.BookingSessions).HasForeignKey(x => x.UserId).IsRequired(true);

});

	}

}