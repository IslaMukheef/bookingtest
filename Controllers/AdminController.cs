using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;
public class AdminController : Controller
{
	private readonly ApplicationDbContext _db;

	public AdminController(ApplicationDbContext db)
	{
		_db = db;
	}

	public record AddRequest(string Title, string Description, decimal Price, int CityId, IFormFile Image);
	[HttpPost]
	public IActionResult Add(AddRequest request)
	{
		var house = new House()
		{
			HouseTitle = request.Title,
			Description = request.Description,
			CityId = request.CityId,
			PricePerDay = request.Price,
			
		};
		_db.Add(house);
		_db.SaveChanges();

		Directory.CreateDirectory("assets");
		using (var stream = System.IO.File.Create(Path.Combine("assets", house.Id + ".png")))
		{
			request.Image.CopyTo(stream);
		}

		return Redirect(Request.Headers["Referer"].ToString());
	}
	[HttpPost]
	public IActionResult Delete(int id)
	{
		var house = _db.Houses.FirstOrDefault(x => x.Id == id);
		System.IO.File.Delete(Path.Combine("assets", house.Id + ".png"));
		if (house == null)
			return NotFound();

		_db.Remove(house);
		_db.SaveChanges();
		return Redirect(Request.Headers["Referer"].ToString());

	}
	public IActionResult Index()
	{
		return View();
	}
}
