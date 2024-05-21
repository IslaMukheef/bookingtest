using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System.Security.Claims;

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
        Console.WriteLine(request.ToString());
        return Redirect("/");
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    public record LoginRequest(string Email, string Password);

    [HttpPost]
    public IActionResult Singin(LoginRequest request)
    {
        Console.WriteLine(request.ToString());

        var user = _db.Users.FirstOrDefault(x => x.Email == request.Email);
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
           return SignIn(claimsPrinciple,new(){});
        }

        return Redirect("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
    }


    [HttpGet]
    public IActionResult Singin()
    {
        return View();
    }
}
