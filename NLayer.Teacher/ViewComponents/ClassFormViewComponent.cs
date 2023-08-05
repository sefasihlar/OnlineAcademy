using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.ClassDtos;

namespace NLayer.Teacher.ViewComponents
{
    public class ClassFormViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> Invoke()
        {
            var values = new ClassDto()
            {

            };


            return View(values);
        }
    }
}
