using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs.ClassDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class ClassListViewComponent:ViewComponent
    {
        private readonly IClassService _classService;
        private readonly IMapper _mapper;

        public ClassListViewComponent(IClassService classService, IMapper mapper)
        {
            _classService=classService;
            _mapper=mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(ClassDto dto)
        {
            var clasList = await _classService.GetAllAsycn();

            var classes = clasList.Where(a => a.Condition == true).ToList();
            //ikinci virgülden sonraki kısım Listede görünecek kısım
            ViewBag.classes = new SelectList(classes, "Id", "Name");
            return View(dto);
        }
    }
}
