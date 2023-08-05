using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.TotalCountDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class TotalExamViewComponent:ViewComponent
    {
        private readonly IExamService _examService;
        private readonly IMapper _mapper;

        public TotalExamViewComponent(IExamService examService, IMapper mapper)
        {
            _examService=examService;
            _mapper=mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var examList = await _examService.GetAllAsycn();
            var values = new TotalCountsDto()
            {
                TotalExam = examList.Count()

            };

            return View(values);
        }
    }
}
