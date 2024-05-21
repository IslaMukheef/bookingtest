using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System.Security.Claims;

namespace backend.Controllers;
public record PassingObject(int HouseId);

public class ResultController : Controller
{
	[FromQuery]
	public int CityId { get; set; }
	public IActionResult Index()
	{
		ViewData[nameof(CityId)] = CityId;
		return View();
	}
	public IActionResult Availability(int houseId)
	{
		PassingObject pass = new(houseId);
		return View(pass);
	}
	public IActionResult Done()
	{
		var isUser = this.User.FindFirst(x => x.Type == ClaimTypes.Sid) != null;
		if (isUser)
		{
            return View();
		}
		else
		{

			return Redirect("/account/singin");
		}

	}
	public IActionResult Search()
	{
		return View();
	}

}
