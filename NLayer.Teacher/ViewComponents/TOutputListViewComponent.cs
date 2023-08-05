using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs.OutputDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class TOutputListViewComponent:ViewComponent
    {
        private readonly IOutputService _OutputService;
        private readonly IMapper _mapper;

        public TOutputListViewComponent(IOutputService outputService, IMapper mapper)
        {
            _OutputService=outputService;
            _mapper=mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(OutputDto dto)
        {
            var outputList = await _OutputService.GetAllAsycn();
            var output = outputList.Where(a => a.Condition == true);
            ViewBag.outputs = new SelectList(output, "Id", "Name");
            return View(dto);
        }
    }
}
