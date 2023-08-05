using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using NLayer.Core.Concrate;
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
            var values = await _questionService.GetByIdAsycn(id);

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


            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Template\\video", file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //Question tablosundaki solution condition durumunu güncelle

            var questionId = await _questionService.GetByIdAsycn(model.QuestionId);

            if (questionId != null)
            {
                questionId.SolutionCondition = questionModel.SolutionCondition;
            }


            if (values != null)
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

            var options = await _optionService.GetAllAsycn();
            ViewBag.options = new SelectList(options, "Id", "Name");
            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Soru çözümü eklenemedi.Lütfen daha sonra tekrar deneyiniz.",
                Css = "error"
            });
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var optionList = await _optionService.GetAllAsycn();
            var classId = await _classService.GetAllAsycn();
            var question = await _questionService.GetByIdAsycn(id);
            var values = await _solutionService.GetByQuestionId(id);
            if (values == null)
            {
                return NotFound();
            }

            ViewBag.QuestionId = question.Id;
            var options = optionList.Where(x => x.QuestionId == question.Id);
            ViewBag.options = new SelectList(options, "Id", "Text");
            return View(new SolutionDto()
            {
                Id = values.Id,
                Text = values.Text,
                VideoUrl = values.VideoUrl,
                QuestionId = values.QuestionId,
                OptionId = values.OptionId,
                Condition = values.Condition,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(SolutionDto model)
        {
            var values = await _solutionService.GetByIdAsycn(model.Id);
            var valuesDto = _mapper.Map<SolutionDto>(values);

            if (values == null)
            {
                return NotFound();
            }
            if (values != null)
            {
                model.Text = values.Text;
                model.VideoUrl = values.VideoUrl;
                model.QuestionId = values.QuestionId;
                model.OptionId = values.OptionId;
                model.Condition =(bool)values.Condition;

                _solutionService.UpdateAsync(valuesDto);
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Başarılı",
                    Message = "Soru çözümü başarıyla güncellendi",
                    Css = "success"
                });
                return RedirectToAction("Index", "Solution");
            }

            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Soru çözümü güncellenemedi.Lütfen bilgileri gözden geçiriniz",
                Css = "error"
            });
            return RedirectToAction("Index", "Solution");
        }

        public async Task<IActionResult> Questions(int LessonId, int SubjectId, int OutputId)
        {

            var questionList = await _questionService.GetWithList();
            var questionListEntitiy = _mapper.Map<List<Question>>(questionList);

            if (LessonId != 0 && SubjectId != 0 && OutputId != 0)
            {
                var val = new QuestionListDto()
                {
                    Questions =questionListEntitiy.OrderByDescending(x => x.Id)
                    .Where(x => x.LessonId == LessonId)
                    .Where(x => x.SubjectId == SubjectId)
                    .Where(x => x.OutputId == OutputId)
                    .ToList()

                };

                return View(val);
            }

            else if (LessonId != 0 && SubjectId == 0 && OutputId == 0)
            {
                var val = new QuestionListDto()
                {
                    Questions = questionListEntitiy.OrderByDescending(x => x.Id)
                  .Where(x => x.LessonId == LessonId)
                  .ToList()

                };

                return View(val);
            }

            else if (LessonId != 0 && SubjectId != 0 && OutputId == 0)
            {
                var val = new QuestionListDto()
                {
                    Questions = questionListEntitiy.OrderByDescending(x => x.Id)
                  .Where(x => x.LessonId == LessonId)
                  .Where(x => x.SubjectId == SubjectId)
                  .ToList()

                };

                return View(val);
            }

            var values = new QuestionListDto()
            {
                Questions =(List<Question>)questionListEntitiy.OrderByDescending(x => x.Id)

            };

            return View(values);
        }

    }
}

