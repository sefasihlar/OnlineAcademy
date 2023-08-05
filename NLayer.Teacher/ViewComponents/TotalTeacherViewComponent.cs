using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.TotalCountDtos;

namespace NLayer.Teacher.ViewComponents
{
    public class TotalTeacherViewComponent:ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public TotalTeacherViewComponent(UserManager<AppUser> userManager)
        {
            _userManager=userManager;
        }

        public IViewComponentResult Invoke()
        {
            var values = new TotalCountsDto()
            {
                TotalTeacher = _userManager.Users.Where(x => x.Authority == true).Count()
            };
            return View(values);
        }
    }
}
