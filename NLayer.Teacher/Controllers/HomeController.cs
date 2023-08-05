using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.TotalCountDtos;
using NLayer.Core.DTOs.TotalCountStudentDtos;
using NLayer.Core.Services;
using NLayer.Teacher.Models;
using System.Diagnostics;

namespace NLayer.Teacher.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISolutionService _solutionService;
        private readonly IQuestionService _questionService;
        private readonly IScorsService _scorsService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(UserManager<AppUser> userManager, ISolutionService solutionService, IQuestionService questionService, IScorsService scorsService, ILogger<HomeController> logger)
        {
            _userManager=userManager;
            _solutionService=solutionService;
            _questionService=questionService;
            _scorsService=scorsService;
            _logger=logger;
        }

        public async Task<IActionResult> Index()
        {
            var questionList = await _questionService.GetAllAsycn();
            var solutionList = await _solutionService.GetAllAsycn();


            var values = new TotalCountsDto()
            {
                TotalQuestion = questionList.Count(),
                TotalSolution = solutionList.Count()
            };

            return View(values);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> StudentIndex()
        {
            var scorsList = await _scorsService.GetAllAsycn();

            var user = await _userManager.GetUserAsync(HttpContext.User);
            var values = new TotalCountStudentDto()
            {
                TotalTure = scorsList.Where(x => x.UserId == user.Id).Sum(x => x.True),
                TotalFalse = scorsList.Where(x => x.UserId == user.Id).Sum(x => x.False)
            };

            return View(values);
        }
    }
}