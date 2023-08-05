using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.BranchDtos;

namespace NLayer.Teacher.ViewComponents
{
    public class BranchFormViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> Invoke()
        {
            var values = new BranchDto()
            {

            };


            return View(values);
        }
    }
}
