

namespace backend.Models;

public class House
{
    public int Id { get; set; }

    public required string HouseTitle { get; set; }
    public required string Description { get; set; }
    public decimal PricePerDay { get; set; }

    public int CityId { get; set; }


    public virtual City City { get; set; } = null!;
    public virtual ICollection<BookingSession> BookingSessions { get; set; } = new HashSet<BookingSession>();

}