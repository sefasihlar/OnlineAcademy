using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.TotalCountDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class TotalSolutionViewComponent : ViewComponent
    {
        private readonly ISolutionService _solutionService;
        private readonly IMapper _mapper;

        public TotalSolutionViewComponent(ISolutionService solutionService, IMapper mapper)
        {
            _solutionService=solutionService;
            _mapper=mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var solutionList = await _solutionService.GetAllAsycn();
            var values = new TotalCountsDto()
            {
                TotalSolution = solutionList.Count()
            };


            return View(values);
        }
    }
}
