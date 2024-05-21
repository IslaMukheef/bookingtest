using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace backend.Controllers;

public class AccountController : Controller
{
	private readonly IPasswordHasher _hasher;
	private readonly ApplicationDbContext _db;

	public AccountController(IPasswordHasher hasher, ApplicationDbContext db)
	{
		_hasher = hasher;
		_db = db;
	}

	public record RegisterRequest(string FullName, string Email, string Password, string PassportId);
	[HttpPost]
	public IActionResult Register(RegisterRequest request)
	{
		var user = new User()
		{
			FullName = request.FullName,
			Email = request.Email,
			PasswordHash = _hasher.HashPassword(request.Password),
			PassportNumber = request.PassportId
		};
		try
		{
			_db.Add(user);
			_db.SaveChanges();
		}
		catch (Exception e)
		{
			Console.WriteLine(e.Message);
			return View();

		}
		return Redirect("/");
	}
	[HttpGet]
	public IActionResult Register()
	{
		return View();
	}

	public record LoginRequest(string Email, string Password);

	[HttpPost]
	public async Task<IActionResult> Singin(LoginRequest request)
	{

		var user = _db.Users.FirstOrDefault(x => x.Email.ToLower() == request.Email.ToLower());
		if (user is null)
			return Redirect("/NotFound");

		if (_hasher.VerifyPassword(request.Password, user.PasswordHash))
		{
			var claims = new List<Claim>(){
			new Claim(ClaimTypes.Sid,user.Id.ToString()),
			new Claim("Admin",user.IsAdmin.ToString()),
		   };
			var claimsIdentity = new ClaimsIdentity(claims);
			var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);
			await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrinciple);
			if (user.IsAdmin)
				return Redirect("/admin");
			else
				return Redirect("/");
		}

		return Redirect("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
	}


	[HttpGet]
	public async Task<IActionResult> SignOut()
	{
		await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		return Redirect("/");
	}

	[HttpGet]
	public IActionResult Singin()
	{
		return View();
	}
}
