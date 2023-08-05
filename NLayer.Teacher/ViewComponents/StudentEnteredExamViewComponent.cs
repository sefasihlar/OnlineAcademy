using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.StudentEntredExamDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class StudentEnteredExamViewComponent:ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IScorsService _scorsService;

        public StudentEnteredExamViewComponent(UserManager<AppUser> userManager, IScorsService scorsService)
        {
            _userManager=userManager;
            _scorsService=scorsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var scorsList = await _scorsService.GetTogetherList();

            var values = new StudentEntredExamDto()
            {
                TotalBiyoloji = scorsList.Where(x => x.UserId == user.Id && x.Exam.Lesson.Name == "Biyoloji").Count(),
                TotalCografya = scorsList.Where(x => x.UserId == user.Id && x.Exam.Lesson.Name == "Cografya").Count(),
                TotalEdebiyat = scorsList.Where(x => x.UserId == user.Id && x.Exam.Lesson.Name == "Edebiyat").Count(),
                TotalFelsefe = scorsList.Where(x => x.UserId == user.Id && x.Exam.Lesson.Name == "Felsefe").Count(),
                TotalTarih = scorsList.Where(x => x.UserId == user.Id && x.Exam.Lesson.Name == "Tarih").Count(),
                TotalFizik = scorsList.Where(x => x.UserId == user.Id && x.Exam.Lesson.Name == "Fizik").Count(),
                TotalKimya = scorsList.Where(x => x.UserId == user.Id && x.Exam.Lesson.Name == "Kimya").Count(),
                TotalMatematik =scorsList.Where(x => x.UserId == user.Id && x.Exam.Lesson.Name == "Matematik").Count(),
                TotalTukce = scorsList.Where(x => x.UserId == user.Id && x.Exam.Lesson.Name == "Turkçe").Count()
            };

            return View(values);
        }
    }
}

