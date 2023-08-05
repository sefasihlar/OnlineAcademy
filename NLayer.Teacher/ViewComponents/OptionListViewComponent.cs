using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using NLayer.Core.DTOs.OptionDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class OptionListViewComponent:ViewComponent
    {
        private readonly IOptionService optionService;

        public OptionListViewComponent(IOptionService optionService)
        {
            this.optionService=optionService;
        }

        public async Task<IViewComponentResult> InvokeAsync(OptionDto dto)
        {
            var option =await optionService.GetAllAsycn();
            ViewBag.options = new SelectList(option, "Id", "Name");
            return View(dto);
        }
    }
}
