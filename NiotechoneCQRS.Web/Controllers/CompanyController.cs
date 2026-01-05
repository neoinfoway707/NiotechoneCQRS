using Microsoft.AspNetCore.Mvc;

namespace NiotechoneCQRS.Web.Controllers;

public class CompanyController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
