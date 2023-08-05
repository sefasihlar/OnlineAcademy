using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.ExamDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class ExamInfoViewComponent:ViewComponent
    {
        private readonly IExamService _examService;
        private readonly IMapper _mapper;

        public ExamInfoViewComponent(IExamService examService, IMapper mapper)
        {
            _examService=examService;
            _mapper=mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (ViewBag.examId != null)
            {
                var values =await _examService.GetByIdAsycn(ViewBag.examId);

                return View(new ExamDto()
                {
                    Id = ViewBag.examId,
                    Title = values.Title,
                    Description = values.Description,
                    Class = values.Class,
                    Lesson = values.Lesson,
                    Timer = values.Timer,
                    ExamDate = values.ExamDate,
                });
            }

            return View(new ExamDto());

        }
    }
}
