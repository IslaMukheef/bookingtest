

namespace backend.Models;

public class House
{
    public int Id { get; set; }
    public int HouseNumber { get; set; }

    public required string Description { get; set; }
    public decimal PricePerDay { get; set; }

    public int UserId { get; set; }
    public int CityId { get; set; }


    public virtual City City { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    public virtual ICollection<BookingSession> BookingSessions { get; set; } = new HashSet<BookingSession>();

}