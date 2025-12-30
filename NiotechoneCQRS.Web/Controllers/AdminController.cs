using Microsoft.AspNetCore.Mvc;

namespace NiotechoneCQRS.Web.Controllers;

public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
