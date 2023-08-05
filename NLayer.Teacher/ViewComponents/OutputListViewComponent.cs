using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs.OutputDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class OutputListViewComponent:ViewComponent
    {
        private readonly IOutputService _outputService;

        public OutputListViewComponent(IOutputService outputService)
        {
            _outputService=outputService;
        }

        public async Task<IViewComponentResult> InvokeAsync(OutputDto dto)
        {
            var output = await _outputService.GetAllAsycn();
            ViewBag.outputs = new SelectList(output, "Id", "Name");
            return View(dto);
        }
    }
}
