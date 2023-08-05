using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.AccountDtos;

namespace NLayer.Teacher.ViewComponents
{
    public class RegisterFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(RegisterDto model)
        {
            var values = new RegisterDto()
            {

            };

            return View(values);
        }
    }
}
