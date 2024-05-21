using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using backend.Models;

namespace backend.Controllers;

public class ResultsController : Controller
{
    public IActionResult Search()
    {
        return View();
    }

}
