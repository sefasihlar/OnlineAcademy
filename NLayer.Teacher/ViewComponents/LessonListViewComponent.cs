using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs.LessonDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class LessonListViewComponent:ViewComponent
    {
        private readonly ILessonService _lessonService;

        public LessonListViewComponent(ILessonService lessonService)
        {
            _lessonService=lessonService;
        }

        public async Task<IViewComponentResult> InvokeAsync(LessonDto dto)
        {
            var lessonList = await _lessonService.GetAllAsycn();
            var lesson = lessonList.Where(a => a.Condition == true);
            ViewBag.lessons = new SelectList(lesson, "Id", "Name");
            return View(dto);
        }
    }
}
