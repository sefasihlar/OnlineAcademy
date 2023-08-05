using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.TotalCountDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class ManyExamWhichClassViewComponent:ViewComponent
    {
        private readonly IExamService _service;

        public ManyExamWhichClassViewComponent(IExamService service)
        {
            _service=service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var examList = await _service.GetWithList();
            var values = new TotalCountsDto()
            {
                ManyExamWhichClass9 = examList.Where(x => x.Class.Name == "9").Count(),
                ManyExamWhichClass10 = examList.Where(x => x.Class.Name == "10").Count(),
                ManyExamWhichClass11 = examList.Where(x => x.Class.Name == "11").Count(),
                ManyExamWhichClass12 = examList.Where(x => x.Class.Name == "12").Count(),
            };

            return View(values);
        }
    }
}
