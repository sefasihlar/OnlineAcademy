using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ExamAnswersDtos;
using NLayer.Core.DTOs.ScorsDtos;
using NLayer.Core.DTOs.SolutionDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.Controllers
{
    public class ExamResultController : Controller
    {
        private readonly IExamAnswersService _examAnswersService;
        private readonly ISolutionService _solutionService;
        private readonly IScorsService _scorsService;
        private readonly ICartService _cartService;
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;

        public ExamResultController(IExamAnswersService examAnswersService, ISolutionService solutionService, IScorsService scorsService, ICartService cartService, IAppUserService appUserService, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper)
        {
            _examAnswersService=examAnswersService;
            _solutionService=solutionService;
            _scorsService=scorsService;
            _cartService=cartService;
            _appUserService=appUserService;
            _userManager=userManager;
            _roleManager=roleManager;
            _mapper=mapper;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);
            var getId = await _appUserService.GetByIdAsycn(Convert.ToInt32(userId));

            var scorsList = await _scorsService.GetTogetherList();

            var values = new ScorListDto()
            {
                scors =scorsList
                .Where(a => a.UserId == getId.Id).ToList()
                .Where(a => a.Condition== true).ToList()
            };

            return View(values);

        }

        [HttpGet]
        public async Task<IActionResult> ResultQuestions(int id)
        {
            var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);
            var getId = await _appUserService.GetByIdAsycn(Convert.ToInt32(userId));

            ViewBag.userId = getId.Id;

            var nullQuestion = 0;
            var score = 0;
            var questionFalse = 0;
            var questionTrue = 0;
            var trueOption = "";
            var correctAnswers = new List<string>();

            var examAnswersList = await _examAnswersService.GetListTogether();
            var examAnswers = examAnswersList.Where(x => x.ExamId == id & x.UserId == getId.Id).ToList();


            if (examAnswers == null)
            {
                return NotFound();
            }

            var solutions = await _solutionService.GetWithQuestionList();


            foreach (var examAnswer in examAnswers)
            {
                if (examAnswer.OptionId != null)
                {
                    var solution = solutions.FirstOrDefault(s => s.Question.Id == examAnswer.Question.Id && s.OptionId == examAnswer.Option.Id);

                    var TrueOption = solutions.FirstOrDefault(s => s.Question.Id == examAnswer.QuestionId).Option.Name;
                    correctAnswers.Add(TrueOption);

                    if (solution != null)
                    {
                        score += 5;
                        questionTrue += 1;
                    }

                    else if (solution == null)
                    {
                        questionFalse += 1;
                    }

                    else
                    {
                        nullQuestion += 1;
                    }

                }
                else
                {
                    var TrueOption = solutions.FirstOrDefault(s => s.Question.Id == examAnswer.QuestionId).Option.Name;
                    correctAnswers.Add(TrueOption);
                    nullQuestion += 1;
                }

            }

            var model = new ExamAnswersListDto()
            {
                ExamAnswers = examAnswers,
                QuestionFalse = questionFalse,
                QuestionTrue = questionTrue,
                QuestionNull = nullQuestion,
                Score = score,
                CorrectAnswers = correctAnswers,

            };

            return View(model);

        }

        public async Task<IActionResult> ResultVideo(int id)
        {
            var solutionList = await _solutionService.GetAllAsycn();
            var questionId = solutionList.FirstOrDefault(x => x.QuestionId == id);

            var values = new SolutionDto()
            {
                QuestionId = id,
                Text = questionId.Text,
                VideoUrl = questionId.VideoUrl,
            };
            return View(values);
        }


        public async Task<IActionResult> ExamScor(int id, int LessonId, int UserId)
        {
            var scorsList = await _scorsService.GetTogetherList();
            var Scors = scorsList.Where(x => x.ExamId == id & x.UserId == UserId).ToList();



            if (LessonId == 0)
            {
                LessonId = 4;
            }

            ViewBag.lessonId = LessonId;

            ViewBag.examId = id;

            ViewBag.userId = UserId;

            var values = new ScorListDto()
            {
                scors = Scors,
                LessonId = LessonId,
            };

            if (values != null)
            {
                return View(values);
            }
            //hata mesajı verilecek
            return View(values);

        }

        public async Task<IActionResult> AllStudentExamResult(int id)
        {
            ViewBag.examId = id;

            var scorsList = await _scorsService.GetTogetherList();
            var Scors = scorsList.Where(x => x.ExamId == id).ToList();

            var values = new ScorListDto()
            {
                scors = Scors
            };

            if (values.scors.Any(x => x.Condition == false))
            {
                ViewBag.condition = false;
            }

            if (values.scors.Any(x => x.Condition == true))
            {
                ViewBag.condition = true;
            }


            if (values != null)
            {
                return View(values);
            }
            //hata mesajı verilecek
            return View();
        }

    }
}
