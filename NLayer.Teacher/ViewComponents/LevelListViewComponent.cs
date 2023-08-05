using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs.LevelDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class LevelListViewComponent:ViewComponent
    {
        private readonly ILevelService _levelService;

        public LevelListViewComponent(ILevelService levelService)
        {
            _levelService=levelService;
        }

        public async Task<IViewComponentResult> InvokeAsync(LevelDto dto)
        {
            var level =await _levelService.GetAllAsycn();
            ViewBag.levels = new SelectList(level, "Id", "Name");
            return View(dto);
        }

    }
}
