using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.TotalCountDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class TotalClassViewComponent:ViewComponent
    {
        private readonly IClassService _classService;
        private readonly IMapper _mapper;

        public TotalClassViewComponent(IClassService classService, IMapper mapper)
        {
            _classService=classService;
            _mapper=mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var classList = await _classService.GetClassBranchList();

            var values = new TotalCountsDto()
            {
                TotalClass = classList.Count(),
            };

            return View(values);
        }
    }
}
