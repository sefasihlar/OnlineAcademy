using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.TotalCountStudentDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class StudentSuccessRateViewComponent:ViewComponent
    {
        private readonly IScorsService _scorsService;
        private readonly UserManager<AppUser> _userManager;

        public StudentSuccessRateViewComponent(IScorsService scorsService, UserManager<AppUser> userManager)
        {
            _scorsService=scorsService;
            _userManager=userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var scorsList = await _scorsService.GetAllAsycn();
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var values = new TotalCountStudentDto()
            {
                TotalTure = scorsList.Where(x => x.UserId == user.Id).Sum(x => x.True),
                TotalFalse =scorsList.Where(x => x.UserId == user.Id).Sum(x => x.False)
            };

            return View(values);
        }
    }
}
