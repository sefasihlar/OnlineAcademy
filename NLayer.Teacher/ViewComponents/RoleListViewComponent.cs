using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.RoleDtos;

namespace NLayer.Teacher.ViewComponents
{
    public class RoleListViewComponent : ViewComponent
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleListViewComponent(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var values = new AppRoleListDto()
            {
                Roles = _roleManager.Roles.ToList()
            };
            return View(values);
        }
    }
}
