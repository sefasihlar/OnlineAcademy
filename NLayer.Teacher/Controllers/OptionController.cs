using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.MessageDtos;
using NLayer.Core.DTOs.OptionDtos;
using NLayer.Core.Services;
using NLayer.WebUI.Extensions;

namespace NLayer.Teacher.Controllers
{
    public class OptionController : Controller
    {
        private readonly IOptionService _optionService;
        private readonly IMapper _mapper;

        public OptionController(IOptionService optionService, IMapper mapper)
        {
            _optionService=optionService;
            _mapper=mapper;
        }

        public async Task<IActionResult> Index()
        {

            var optionList = await _optionService.GetAllAsycn();
            var optionListDto = _mapper.Map<List<OptionDto>>(optionList);
            var values = new OptionListDto()
            {
                Options =optionListDto
            };
            if (values == null)
            {
                return NotFound();
            }
            return View(values);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OptionDto dto)
        {
            if (ModelState.IsValid)
            {

                var values = _mapper.Map<Option>(dto);

                if (values != null)
                {
                    await _optionService.AddAsycn(values);

                    TempData.Put("message", new ResultMessageDto()
                    {
                        Title = "Başarılı",
                        Message = "Şık ekleme işlemi başarılı",
                        Css = "success"
                    });
                    return RedirectToAction("Index", "Option");
                }

            }
            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Şık eklemek işlemi başarısız.Lütfen bilgilerinizi gözden geçiriniz",
                Css = "error"
            });
            return View(dto);
        }

        public async Task<IActionResult> Delete(OptionDto dto)
        {
            var values = await _optionService.GetByIdAsycn(dto.Id);
            if (values != null)
            {
                await _optionService.RemoveAsycn(values);
                return RedirectToAction("Index", "Option");
            }

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var values = await _optionService.GetByIdAsycn(id);

            if (values == null)
            {
                return NotFound();
            }

            var optionDto = _mapper.Map<OptionDto>(values);

            return View(optionDto);

        }
    }
}
