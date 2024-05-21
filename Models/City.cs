

namespace backend.Models;

public class City
{
    public int Id { get; set; }
    public required string CityName { get; set; }
    public virtual ICollection<House> Houses { get; set; } = new HashSet<House>();

}
