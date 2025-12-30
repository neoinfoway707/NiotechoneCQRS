using Microsoft.AspNetCore.Mvc;

namespace NiotechoneCQRS.Web.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
