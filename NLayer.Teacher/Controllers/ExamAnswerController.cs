using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ExamAnswersDtos;
using NLayer.Core.DTOs.MessageDtos;
using NLayer.Core.Services;
using NLayer.WebUI.Extensions;

namespace NLayer.Teacher.Controllers
{
    public class ExamAnswerController : Controller
    {
        private readonly IExamAnswersService _examAnswersService;
        private readonly IExamQuestionsService _examQuestionsService;
        private readonly ICartService _cartService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public ExamAnswerController(IExamAnswersService examAnswersService, IExamQuestionsService examQuestionsService, ICartService cartService, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _examAnswersService=examAnswersService;
            _examQuestionsService=examQuestionsService;
            _cartService=cartService;
            _userManager=userManager;
            _roleManager=roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(ExamAnswersDto dto)
        {
            if (dto != null)
            {
                ViewBag.ExamModel = dto.ExamId;
                return View(dto);
            }

            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> Create(ExamAnswersDto dto, int[] questionIds, Dictionary<int, int> optionIds)
        {
            if (dto != null && questionIds != null && optionIds != null)
            {
                var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);

                var getId = userId;
                dto.UserId =Convert.ToInt32(getId);

                foreach (var item in questionIds)
                {
                    //TryGetValue değeri kontrol edip varsa atama yapar yoksa değeri null yaparak geçer
                    if (optionIds.TryGetValue(item, out int option))
                    {
                        _examAnswersService.Create(dto, item, option);
                    }
                    else
                    {
                        _examAnswersService.Create(dto, item, null);
                    }
                }

                _cartService.AddToCart(Convert.ToString(getId), dto.ExamId);
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Sinav Bitti",
                    Message = "Sinaviniz bitti.Sinav sonucunuza Sinav sonuclarım kismindan ulasabilirsiniz :)",
                    Css = "warning"
                });
                return RedirectToAction("Index", "Home");
            }
            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Sinavinizda hata ile karsilastik lütfen yetkili birimler iletisime geciniz",
                Css = "error"
            });
            return View(dto);
        }
    }
}
