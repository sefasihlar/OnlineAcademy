using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.GuardianDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class InfoGuardianViewComponent:ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserService _appUserService;
        private readonly IGuardianService _guardianService;
        private readonly IMapper _mapper;

        public InfoGuardianViewComponent(UserManager<AppUser> userManager, IAppUserService appUserService, IGuardianService guardianService, IMapper mapper)
        {
            _userManager=userManager;
            _appUserService=appUserService;
            _guardianService=guardianService;
            _mapper=mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);
            var getId =await _appUserService.GetByIdAsycn(Convert.ToInt32(userId));
            var guardianList = await _guardianService.GetAllAsycn();
            var info = guardianList.FirstOrDefault(x => x.UserId == getId.Id);


            var values = new GuardianDto()
            {
                GuardianName = info.GuardianName,
            };
            return View();
        }
    }
}
