using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.LevelDtos;
using NLayer.Core.DTOs.MessageDtos;
using NLayer.Core.Services;
using NLayer.WebUI.Extensions;

namespace NLayer.Teacher.Controllers
{
    public class LevelController : Controller
    {
        private readonly ILevelService _levelService;
        private readonly IMapper _mapper;

        public LevelController(ILevelService levelService, IMapper mapper)
        {
            _levelService=levelService;
            _mapper=mapper;
        }

        public async Task<IActionResult> Index()
        {
            var levels = await _levelService.GetAllAsycn();
            var levelsDto = _mapper.Map<LevelDto>(levels);

            return View(levelsDto);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LevelDto levelDto)
        {
            if (ModelState.IsValid)
            {
                var values = _mapper.Map<Level>(levelDto);

                await _levelService.AddAsycn(values);

                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Başarılı",
                    Message = "Derece ekleme işlemi başarılı",
                    Css = "success"
                });
                return RedirectToAction("Index", "Level");
            }

            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Derece ekleme işlemi başarısız",
                Css = "error"
            });

            return View(levelDto);
        }

        public async Task<IActionResult> Delete(LevelDto LevelDto)
        {
            var values = await _levelService.GetByIdAsycn(LevelDto.Id);

            if (values != null)
            {
                await _levelService.RemoveAsycn(values);

                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Başarılı",
                    Message = "Derece silme işlemi başarılı",
                    Css = "success"
                });

                return RedirectToAction("Index", "Level");
            }

            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Derece silme işlemi başarısız",
                Css = "error"
            });
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(LevelDto levelDto)
        {
            var values = await _levelService.GetByIdAsycn(levelDto.Id);

            if (values == null)
            {
                return NotFound();
            }

            var valuesDto = _mapper.Map<LevelDto>(values);

            return View(valuesDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(LevelDto levelDto)
        {
            if (ModelState.IsValid)
            {
                var values = await _levelService.GetByIdAsycn(levelDto.Id);

                if (values == null)
                {
                    TempData.Put("message", new ResultMessageDto()
                    {
                        Title = "Hata",
                        Message = "Derece bulunamadı.Bilgilerinizi gözden geçiriniz",
                        Css = "error"
                    });
                }

                values.Name = levelDto.Name;
                values.Condition = levelDto.Condition;
                values.UpdatedDate = levelDto.UpdatedDate;

                await _levelService.UpdateAsycn(values);

                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Başarılı",
                    Message = "Derece güncelleme işlemi başarılı",
                    Css = "success"
                });
            }
            return RedirectToAction("Index", "Level");
        }
    }
}
