using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.TotalLessonExamsDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class TotalLessonExamViewComponent:ViewComponent
    {
        private readonly IExamService _examService;
        private readonly IMapper _mapper;

        public TotalLessonExamViewComponent(IExamService examService, IMapper mapper)
        {
            _examService=examService;
            _mapper=mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {


            var examList = await _examService.GetWithList();
            var values = new TotalLessonExamsDto()
            {
                TotalBiyoloji = examList.Count(x => x.Lesson?.Name == "Biyoloji"),
                TotalFizik =examList.Count(x => x.Lesson?.Name == "Fizik"),
                TotalKimya = examList.Count(x => x.Lesson?.Name == "Kimya"),
                TotalFelsefe = examList.Count(x => x.Lesson?.Name == "Felsefe"),
                TotalTukce = examList.Count(x => x.Lesson?.Name == "Türkçe"),
                TotalEdebiyat =examList.Count(x => x.Lesson?.Name == "Edebiyat"),
                TotalCografya =examList.Count(x => x.Lesson?.Name == "Coğrafya"),
                TotalTarih = examList.Count(x => x.Lesson?.Name == "Tarih"),
                TotalMatematik = examList.Count(x => x.Lesson?.Name == "Matematik")
            };

            return View(values);
        }
    }
}
