using Microsoft.AspNetCore.Mvc;

namespace NLayer.Academy.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
