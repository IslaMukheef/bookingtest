

namespace backend.Models;

public class User
{
    public int Id { get; set; }
    public required string FullName { get; set; }

    public required string PasswordHash { get; set; }

    public required string Email { get; set; }

    public required string PassportNumber { get; set; }

    public bool IsAdmin { get; set; } = false;

    public virtual ICollection<House> Houses { get; set; } = new HashSet<House>();
    public virtual ICollection<BookingSession> BookingSessions { get; set; } = new HashSet<BookingSession>();


}
