using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.ScorsDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class StudentAchievementRankingViewComponent : ViewComponent
    {
        private readonly IScorsService _scorsService;

        public StudentAchievementRankingViewComponent(IScorsService scorsService)
        {
            _scorsService=scorsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var scorList = await _scorsService.GetTogetherList();
            var values = new ScorListDto()
            {
                scors = scorList
                .Where(a => a.ExamId == ViewBag.examId)
                .Where(a => a.Condition == true)
                .ToList(),
            };

            return View(values);
        }
    }
}
