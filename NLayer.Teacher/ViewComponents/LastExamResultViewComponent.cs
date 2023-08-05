using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ScorsDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class LastExamResultViewComponent:ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IScorsService _scorsService;

        public LastExamResultViewComponent(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IScorsService scorsService)
        {
            _userManager=userManager;
            _roleManager=roleManager;
            _scorsService=scorsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()

        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var scorsList = await _scorsService.GetTogetherList();
            var latestScore = scorsList.OrderByDescending(s => s.ExamId).FirstOrDefault();
            var values = new ScorListDto();

            if (await _userManager.IsInRoleAsync(user, "Öğrenci"))
            {
                values.scors = scorsList.Where(s => s.User.ClassId == user.ClassId
                && s.ExamId == latestScore.ExamId
                && s.Condition == true).ToList();
            }
            else if (await _userManager.IsInRoleAsync(user, "Öğretmen") ||
                     await _userManager.IsInRoleAsync(user, "Müdür") ||
                     await _userManager.IsInRoleAsync(user, "MüdürYardımcısı"))
            {
                values.scors = scorsList.Where(s => s.ExamId == latestScore.ExamId).ToList();
            }

            return View(values);
        }
    }
}
