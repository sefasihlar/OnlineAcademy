using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.StudentTotalExamRateDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class StudentTotalExamRateViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IScorsService _scorsService;

        public StudentTotalExamRateViewComponent(UserManager<AppUser> userManager, IScorsService scorsService)
        {
            _userManager=userManager;
            _scorsService=scorsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var scorslist = await _scorsService.GetTogetherList();
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var biyolojiScores = scorslist.Where(x => x.UserId == user.Id && x.Exam.Lesson.Name == "Biyoloji");
            var cografyaScores = scorslist.Where(x => x.UserId == user.Id && x.Exam.Lesson.Name == "Coğrafya");
            var edebiyatScores = scorslist.Where(x => x.UserId == user.Id && x.Exam.Lesson.Name == "Edebiyat");
            var felsefeScores = scorslist.Where(x => x.UserId == user.Id && x.Exam.Lesson.Name == "Felsefe");
            var fizikScores = scorslist.Where(x => x.UserId == user.Id && x.Exam.Lesson.Name == "Fizik");
            var kimyaScores = scorslist.Where(x => x.UserId == user.Id && x.Exam.Lesson.Name == "Kimya");
            var matematikScores = scorslist.Where(x => x.UserId == user.Id && x.Exam.Lesson.Name == "Matematik");
            var tarihScores = scorslist.Where(x => x.UserId == user.Id && x.Exam.Lesson.Name == "Tarih");
            var turkceScores = scorslist.Where(x => x.UserId == user.Id && x.Exam.Lesson.Name == "Türkçe");

            var values = new StudentTotalExamRateDto()
            {
                TotalBiyoloji = biyolojiScores.Any() ? Math.Round(biyolojiScores.Sum(x => x.scors.Average) / biyolojiScores.Count() * 100) : 0,
                TotalCografya = cografyaScores.Any() ? Math.Round(cografyaScores.Sum(x => x.scors.Average) / cografyaScores.Count() * 100) : 0,
                TotalEdebiyat = edebiyatScores.Any() ? Math.Round(edebiyatScores.Sum(x => x.Average) / edebiyatScores.Count() * 100) : 0,
                TotalFelsefe = felsefeScores.Any() ? Math.Round(felsefeScores.Sum(x => x.Average) / felsefeScores.Count() * 100) : 0,
                TotalFizik = fizikScores.Any() ? Math.Round(fizikScores.Sum(x => x.Average) / fizikScores.Count() * 100) : 0,
                TotalKimya = kimyaScores.Any() ? Math.Round(kimyaScores.Sum(x => x.Average) / kimyaScores.Count() * 100) : 0,
                TotalMatematik = matematikScores.Any() ? Math.Round(matematikScores.Sum(x => x.Average) / matematikScores.Count() * 100) : 0,
                TotalTarih = tarihScores.Any() ? Math.Round(tarihScores.Sum(x => x.Average) / tarihScores.Count() * 100) : 0,
                TotalTukce = turkceScores.Any() ? Math.Round(turkceScores.Sum(x => x.Average) / turkceScores.Count() * 100) : 0,



            };

            return View(values);
        }
    }
}
