using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs.BranchDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class BranchListViewComponent:ViewComponent
    {
        private readonly IBranchService _branchService;
        private readonly IMapper _mapper;

        public BranchListViewComponent(IBranchService branchService, IMapper mapper)
        {
            _branchService=branchService;
            _mapper=mapper;
        }

        public async Task<IViewComponentResult> Invoke(BranchDto dto)
        {
            var branchList = await _branchService.GetAllAsycn();
            var branch = branchList.Where(a => a.Condition == true);
            ViewBag.brances = new SelectList(branch, "Id", "Name");
            return View(dto);
        }
    }
}
