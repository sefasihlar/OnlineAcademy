using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ExamAnswersDtos;
using NLayer.Core.DTOs.MessageDtos;
using NLayer.Core.Services;
using NLayer.WebUI.Extensions;

namespace NLayer.Teacher.Controllers
{
    public class CalculateExamScorsController : Controller
    {
        private readonly IExamAnswersService _examAnswersService;
        private readonly ISolutionService _solutionService;
        private readonly IScorsService _scorsService;
        private readonly ICartService _cartService;
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;

        public CalculateExamScorsController(IExamAnswersService examAnswersService, ISolutionService solutionService, IScorsService scorsService, ICartService cartService, IAppUserService appUserService, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IMapper mapper)
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AllUsersResultQuestions(int id)
        {
            // Sınava katılan tüm öğrencilerin listesini al
            var users = _userManager.Users.Where(x => x.Authority == false).ToList();
            // Her kullanıcının sınav sonucunu hesapla ve veritabanına kaydet
            foreach (var user in users)
            {
                // Kullanıcının sınav cevaplarını al
                var userExamAnswersFilter = await _examAnswersService.GetListTogether();
                    
                var userExamAnswers = userExamAnswersFilter.Where(x => x.ExamId == id && x.UserId == user.Id).ToList();

                var nullQuestion = 0;
                var score = 0;
                var questionFalse = 0;
                var questionTrue = 0;
                var trueOption = "";
                var correctAnswers = new List<string>();

                if (userExamAnswers == null)
                {
                    continue;
                }

                foreach (var examAnswer in userExamAnswers)
                {
                    if (examAnswer.OptionId != null)
                    {
                        var solutions =await _solutionService.GetWithQuestionList();
                        var solution = solutions.FirstOrDefault(s => s.Question.Id == examAnswer.Question.Id && s.OptionId == examAnswer.Option.Id);

                        var TrueOption = solutions.FirstOrDefault(s => s.Question.Id == examAnswer.QuestionId).Option.Name;
                        correctAnswers.Add(TrueOption);

                        if (solution != null)
                        {
                            score += 1;
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
                        var solutions =await _solutionService.GetWithQuestionList();
                        var TrueOption = solutions.FirstOrDefault(s => s.Question.Id == examAnswer.QuestionId).Option.Name;
                        correctAnswers.Add(TrueOption);
                        nullQuestion += 1;
                    }
                }

                decimal totalNegative = questionFalse * 0.25m; // 'm' ekleyerek decimal tipinde bir sabit belirtiyoruz
                decimal net = questionTrue - totalNegative;
                decimal totalScore = net * 100m; // 'm' ekleyerek decimal tipinde bir sabit belirtiyoruz
                int totalQuestion = userExamAnswers.Count;
                if (totalScore != 0)
                {
                    decimal ExamScor = Math.Round(totalScore / totalQuestion, 2);
                    var model = new ExamAnswersListDto()
                    {
                        ExamAnswers =_mapper.Map<List<ExamAnswersDto>>(userExamAnswers),
                        QuestionFalse = questionFalse,
                        QuestionTrue = questionTrue,
                        QuestionNull = nullQuestion,
                        Score = score,
                        CorrectAnswers = correctAnswers,
                        UserId = user.Id, // Kullanıcının Id'sini de modelde kaydet
                        ExamId = id // Sınavın Id'sini de modelde kaydet
                    };


                    var values = new Scors()
                    {
                        UserId = model.UserId,
                        ExamId = model.ExamId,
                        True = model.QuestionTrue,
                        False = model.QuestionFalse,
                        Null = model.QuestionNull,
                        Average = net,
                        Scor = ExamScor,
                        Condition = false,
                    };

                    // Modeli veritabanına kaydet
                   await _scorsService.AddAsycn(values);
                    TempData.Put("message", new ResultMessageDto()
                    {
                        Title = "Başarılı",
                        Message = "Hesaplama işlemi başarılı",
                        Css = "success"
                    });

                }
                // sonucu iki ondalık basamakla yuvarlıyoruz

            }

            return RedirectToAction("TeacherExams", "MyExam");
        }

        public async Task<IActionResult> UpdateScors(int id, bool condition)
        {
            var scorsList = await _scorsService.GetAllAsycn();
                
             var scors = scorsList.Where(x => x.ExamId == id).ToList();
            if (scors != null)
            {
                if (condition == true)
                {
                    foreach (var item in scors)
                    {
                        item.Condition = condition;
                       await _scorsService.UpdateAsycn(item);
                    }

                    TempData.Put("message", new ResultMessageDto()
                    {
                        Title = "Başarılı",
                        Message = "Sınav sonuçları başarıyla yayınlandı.",
                        Css = "success"
                    });

                    return RedirectToAction("TeacherExams", "MyExam");
                }

                if (condition == false)
                {
                    foreach (var item in scors)
                    {
                        item.Condition = condition;
                       await _scorsService.UpdateAsycn(item);
                    }

                    TempData.Put("message", new ResultMessageDto()
                    {
                        Title = "Uyarı!",
                        Message = "Sınav sonuçları yayından kaldırıldı.",
                        Css = "warning"
                    });

                    return RedirectToAction("TeacherExams", "MyExam");
                }



                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Hata!",
                    Message = "Sınav yayınlama işleminde bir aksaklık yaşandı.",
                    Css = "error"
                });


                return RedirectToAction("TeacherExams", "MyExam");

            }



            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Sınav sonuçları yayınlanamadı.",
                Css = "error"
            });

            return View();
        }
    }
}
