using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.GuardianDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class GuardianUpdateViewComponent:ViewComponent
    {
        private readonly IGuardianService _guardianService;
        private readonly IMapper _mapper;

        public GuardianUpdateViewComponent(IGuardianService guardianService, IMapper mapper)
        {
            _guardianService=guardianService;
            _mapper=mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var guardianList = await _guardianService.GetWithStudentList();
            var guardian = guardianList.FirstOrDefault(x => x.Id == ViewBag.GuardianId);

            var values = new GuardianDto()
            {
                GuardianName = guardian.GuardianName,
                GuardianPhone = guardian.GuardianPhone,
            };


            return View(values);
        }
    }
}
