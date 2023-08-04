using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.MessageDtos;
using NLayer.Core.DTOs.OutputDtos;
using NLayer.Core.Services;
using NLayer.WebUI.Extensions;

namespace NLayer.Teacher.Controllers
{
    public class OutputController : Controller
    {
        private readonly IOutputService _OutputService;
        private readonly IMapper _Mapper;

        public OutputController(IOutputService outputService, IMapper mapper)
        {
            _OutputService=outputService;
            _Mapper=mapper;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _OutputService.GetWithSubjectList();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OutputDto outputDto)
        {
            var values = _Mapper.Map<Output>(outputDto);

            if (values != null)
            {
                await _OutputService.AddAsycn(values);
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Başarılı",
                    Message = "Ders Kazanımı eklendi",
                    Css = "success"
                });
                return RedirectToAction("Index", "Output");
            }

            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Ders Kazanımı ekleme işlemi başarısız. Lütfen bilgileri gözden geçiriniz",
                Css = "error"
            });
            return View(outputDto);
        }
    }
}
