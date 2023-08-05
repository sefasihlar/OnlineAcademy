using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs.SubjectDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class SubjectListViewComponent:ViewComponent
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;

        public SubjectListViewComponent(ISubjectService subjectService, IMapper mapper)
        {
            _subjectService=subjectService;
            _mapper=mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(SubjectDto dto)
        {
            var subjects =_mapper.Map<List<SubjectDto>>(await _subjectService.GetAllAsycn());
            subjects.Where(a => a.Condition == true);
            ViewBag.subjects = new SelectList(subjects, "Id", "Name");
            return View(dto);
        }
    }
}
