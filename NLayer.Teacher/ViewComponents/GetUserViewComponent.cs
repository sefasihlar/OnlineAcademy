using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.AccountDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class GetUserViewComponent:ViewComponent
    {
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        private UserManager<AppUser> _userManager;

        public GetUserViewComponent(IAppUserService appUserService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _appUserService=appUserService;
            _mapper=mapper;
            _userManager=userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);

            var values =await _appUserService.GetByIdAsycn(Convert.ToInt32(userId));

            return View(new AppUserDto()
            {
                Id = values.Id,
                Name = values.Name,
                SurName = values.SurName,

            });
        }
    }
}
