

namespace backend.Models;

public class BookingSession
{
    public int Id { get; set; }
  
    public DateOnly StartingDate { get; set; }
    public DateOnly EndDate { get; set; }

    public int UserId { get; set; }
    public int HouseId { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual House House { get; set; } = null!;
}
