using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using NLayer.Core.DTOs.MessageDtos;
using NLayer.Core.DTOs.QuestionDtos;
using NLayer.Core.DTOs.SolutionDtos;
using NLayer.Core.Services;
using NLayer.WebUI.Extensions;

namespace NLayer.Teacher.Controllers
{
    public class SolutionController : Controller
    {
        private readonly ISolutionService _solutionService;
        private readonly IOptionService _optionService;
        private readonly IQuestionService _questionService;
        private readonly IClassService _classService;
        private readonly IMapper _mapper;

        public SolutionController(ISolutionService solutionService, IOptionService optionService, IQuestionService questionService, IClassService classService, IMapper mapper)
        {
            _solutionService=solutionService;
            _optionService=optionService;
            _questionService=questionService;
            _classService=classService;
            _mapper=mapper;
        }

        public async Task<IActionResult> Index(int id)
        {
            var values =await _questionService.GetByIdAsycn(id);

            if (values == null)
            {
                return NotFound();
            }
            var options = await _optionService.GetAllAsycn();
            var optionsList = options.Where(x => x.QuestionId == values.Id);
            ViewBag.options = new SelectList(options, "Id", "Text");

            return View(new QuestionDto()
            {
                Id = values.Id,
                Text = values.Text,
                ImageUrl = values.ImageUrl,
                QuestionText = values.QuestionText,
                LessonId = values.LessonId,
                LevelId = values.LevelId,
                SubjectId = values.SubjectId,
                OutputId = values.OutputId,
                Condition =(bool)values.Condition,
                SolutionCondition = values.SolutionCondition,
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [RequestFormLimits(MultipartBodyLengthLimit = 2147483647)]
        [HttpPost]
        public async Task<IActionResult> Create(SolutionDto model, IFormFile file, QuestionDto questionModel)
        {

            var values = new SolutionDto()
            {
                Id = model.Id,
                Text = model.Text,
                VideoUrl = file.FileName,
                QuestionId = model.QuestionId,
                OptionId = model.OptionId,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
                Condition = model.Condition,
            };

            var valuesEntity = _mapper.Map<Solution>(values);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Template\\video", file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //Question tablosundaki solution condition durumunu güncelle

            var questionId =await _questionService.GetByIdAsycn(model.QuestionId);

            if (questionId != null)
            {
                questionId.SolutionCondition = questionModel.SolutionCondition;
            }


            if (valuesEntity != null)
            {
                //Buradaki solution operasyonlarında sıkıntı var bakılıcak
                //await _solutionService.AddAsycn(valuesEntity);

                await _questionService.UpdateAsycn(questionId);
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Başarılı",
                    Message = "Soru çözümü başarıyla eklendi",
                    Css = "success"
                });
                return RedirectToAction("Questions", "Solution");

            }

            var options =await _optionService.GetAllAsycn();
            ViewBag.options = new SelectList(options, "Id", "Name");
            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Soru çözümü eklenemedi.Lütfen daha sonra tekrar deneyiniz.",
                Css = "error"
            });
            return View(model);
        }
    }
}
