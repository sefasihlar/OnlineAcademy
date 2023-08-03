using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.ClassDtos;
using NLayer.Core.Services;
using NLayer.Service.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class ClassDetayViewComponent:ViewComponent
    {
        private readonly IClassService _classService;
        private readonly IMapper _maper;

        public ClassDetayViewComponent(IClassService classService, IMapper maper)
        {
            _classService = classService;
            _maper = maper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            var values = await _classService.GetByIdAsycn(Id);
            var valuesDto = _maper.Map<ClassDto>(values);
            return View(valuesDto);
        }
    }
}
