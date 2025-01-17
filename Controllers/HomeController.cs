
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Technexa.Controllers;

public class HomeController : Controller
{
    public static SelectList? Categories ;
    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.Categories = Categories;


        return View();

    }


}

