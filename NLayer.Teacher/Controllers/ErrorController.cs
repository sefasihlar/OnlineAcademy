using Microsoft.AspNetCore.Mvc;

namespace NLayer.Teacher.Controllers
{
    public class ErrorController: Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
