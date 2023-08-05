using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.ScorsDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class AllExamResultsViewComponent:ViewComponent
    {
        private readonly IScorsService _scorsService;
        private readonly IMapper _mapper;


        public AllExamResultsViewComponent(IScorsService scorsService, IMapper mapper)
        {
            _scorsService=scorsService;
            _mapper=mapper;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IViewComponentResult> InvokeAsync(int LessonId)
        {
            var scorList = await _scorsService.GetTogetherList();


            if (LessonId == 0)
            {
                LessonId = 4;
            }
            var values = new ScorListDto()
            {
                scors = scorList.Where(x => x.Exam.Lesson.Id == ViewBag.LessonId & x.UserId == ViewBag.userId).ToList()
            };

            if (values != null)
            {
                return View(values);
            }

            return View(LessonId);
        }
    }
}
