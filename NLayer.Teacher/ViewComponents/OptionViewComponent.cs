using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.QuestionDtos;

namespace NLayer.Teacher.ViewComponents
{
    public class OptionViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new QuestionDto()
            {
            });
        }
    }
}
