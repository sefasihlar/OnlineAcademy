using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs.LessonDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class TLessonListViewComponent:ViewComponent
    {
        private readonly ILessonService _lessonService;
        private readonly IMapper _mapper;

        public TLessonListViewComponent(ILessonService lessonService, IMapper mapper)
        {
            _lessonService=lessonService;
            _mapper=mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(LessonDto dto)
        {
            var lessonDto = _mapper.Map<List<LessonDto>>(await _lessonService.GetAllAsycn());
            var lesson = lessonDto.Where(a => a.Condition == true);
            ViewBag.lessons = new SelectList(lesson, "Id", "Name");
            return View(dto);
        }
    }
}
