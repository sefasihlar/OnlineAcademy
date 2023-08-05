using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.ScorsDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class ExamResultPercentileViewComponent:ViewComponent
    {
        private readonly IScorsService _scorsService;
        private readonly IMapper _mapper;

        public ExamResultPercentileViewComponent(IScorsService scorsService, IMapper mapper)
        {
            _scorsService=scorsService;
            _mapper=mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var scorsList = await _scorsService.GetAllAsycn();

            var TotalPercent = scorsList.Where(x => x.ExamId == ViewBag.examId).ToList();


            var TotalScorCount = TotalPercent.Count();

            decimal TotalTrue = TotalPercent.Sum(x => x.True);
            decimal TotalFalse = TotalPercent.Sum(x => x.False);
            decimal TotalNull = TotalPercent.Sum(x => x.Null);
            decimal TotalScor = TotalPercent.Sum(x => x.Scor);

            var TotalQuestion = (TotalFalse + TotalNull + TotalTrue);

            var values = new ScorListDto()
            {
                //decimal ExamScor = Math.Round(totalScore / totalQuestion, 2);

                TotalFalsePercentile = Math.Round((TotalFalse / TotalQuestion) * (100), 2),
                TotalTruePercentile = Math.Round((TotalTrue / TotalQuestion) * (100), 2),
                TotalNullPercentile = Math.Round((TotalNull / TotalQuestion) * (100), 2),
                TotalScorPercentile = Math.Round((TotalScor / TotalScorCount), 2),
            };

            return View(values);

        }


    }
}
