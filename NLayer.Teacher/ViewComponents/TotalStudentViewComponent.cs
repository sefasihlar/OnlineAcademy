using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.TotalCountDtos;

namespace NLayer.Teacher.ViewComponents
{
    public class TotalStudentViewComponent:ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public TotalStudentViewComponent(UserManager<AppUser> userManager)
        {
            _userManager=userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var users = new TotalCountsDto()
            {
                TotalStudent = _userManager.Users.Where(x => x.Authority == false).Count()
            };

            return View(users);
        }
    }
}
