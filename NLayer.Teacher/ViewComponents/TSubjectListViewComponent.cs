using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs.SubjectDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class TSubjectListViewComponent:ViewComponent
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;

        public TSubjectListViewComponent(ISubjectService subjectService, IMapper mapper)
        {
            _subjectService=subjectService;
            _mapper=mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(SubjectDto dto)
        {
            var subjectLislt = await _subjectService.GetAllAsycn();
            var subjects = subjectLislt.Where(a => a.Condition == true);
            ViewBag.subjects = new SelectList(subjects, "Id", "Name");
            return View(dto);
        }
    }
}
