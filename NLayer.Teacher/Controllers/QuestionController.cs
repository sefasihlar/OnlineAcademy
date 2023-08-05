using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.LessonDtos;
using NLayer.Core.DTOs.MessageDtos;
using NLayer.Core.DTOs.QuestionDtos;
using NLayer.Core.Services;
using NLayer.WebUI.Extensions;

namespace NLayer.Teacher.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly ILessonService _lessonService;
        private readonly ILevelService _levelService;
        private readonly ISubjectService _subjectService;
        private readonly IOutputService _outputService;
        private readonly IOptionService _optionService;
        private readonly ISolutionService _solutionService;
        private readonly IClassService _classService;
        private readonly IMapper _mapper;

        public QuestionController(IQuestionService questionService, ILessonService lessonService, ILevelService levelService, ISubjectService subjectService, IOutputService outputService, IOptionService optionService, ISolutionService solutionService, IClassService classService, IMapper mapper)
        {
            _questionService=questionService;
            _lessonService=lessonService;
            _levelService=levelService;
            _subjectService=subjectService;
            _outputService=outputService;
            _optionService=optionService;
            _solutionService=solutionService;
            _classService=classService;
            _mapper=mapper;
        }

        public async Task<IActionResult> Index(int LessonId, int SubjectId, int OutputId)
        {
            var questionList = await _questionService.GetWithList();
            var questionListEntitiy = _mapper.Map<List<Question>>(questionList);

            if (LessonId != 0 && SubjectId != 0 && OutputId != 0)
            {
                var val = new QuestionListDto()
                {
                    Questions =questionListEntitiy
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
                    Questions = questionListEntitiy
                  .Where(x => x.LessonId == LessonId)
                  .ToList()

                };

                return View(val);
            }

            else if (LessonId != 0 && SubjectId != 0 && OutputId == 0)
            {
                var val = new QuestionListDto()
                {
                    Questions = questionListEntitiy
                  .Where(x => x.LessonId == LessonId)
                  .Where(x => x.SubjectId == SubjectId)
                  .ToList()

                };

                return View(val);
            }

            var values = new QuestionListDto()
            {
                Questions = questionListEntitiy

            };

            var clases = await _classService.GetAllAsycn();

            ViewBag.clases = new SelectList(clases, "Id", "Name");

            var lessons =await _lessonService.GetAllAsycn();
            ViewBag.lessons = new SelectList(lessons, "Id", "Name");

            var level = await _levelService.GetAllAsycn();
            ViewBag.levels = new SelectList(level, "Id", "Name");

            var subject =await _subjectService.GetAllAsycn();
            ViewBag.subjects = new SelectList(subject, "Id", "Name");

            var output =await _outputService.GetAllAsycn();
            ViewBag.outputs = new SelectList(output, "Id", "Name");

            var option =await _questionService.GetAllAsycn();
            ViewBag.options = new SelectList(option, "Id", "Name");

            return View(values);

        }

        public async Task<IActionResult> FilterList(int LessonId, int SubjectId, int OutputId)
        {
            var questionList = await _questionService.GetWithList();
            var questionListEntitiy = _mapper.Map<List<Question>>(questionList);
            if (LessonId != null && SubjectId != null && OutputId != null)
            {

                var val = new QuestionListDto()
                {
                    Questions = questionListEntitiy
                    .Where(x => x.LessonId == LessonId)
                    .Where(x => x.SubjectId == SubjectId)
                    .Where(x => x.OutputId == OutputId)
                    .ToList()

                };

                return View(val);
            }

            else if (LessonId != null && SubjectId == null && OutputId == null)
            {
                var val = new QuestionListDto()
                {
                    Questions = questionListEntitiy
                  .Where(x => x.LessonId == LessonId)
                  .ToList()

                };

                return View(val);
            }

            else if (LessonId != null && SubjectId != null && OutputId == null)
            {
                var val = new QuestionListDto()
                {
                    Questions = questionListEntitiy
                  .Where(x => x.LessonId == LessonId)
                  .Where(x => x.SubjectId == SubjectId)
                  .ToList()

                };

                return RedirectToAction("Index", "Questioin", val);
            }

            var values = new QuestionListDto()
            {
                Questions =questionListEntitiy

            };

            return View(values);
        }

        public async Task<JsonResult> Class()
        {
            var values = await _classService.GetAllAsycn();
            return Json(values);
        }

        public async Task<JsonResult> Lesson(int id)
        {
            var lessonList =  _lessonService.GetWithClassList();
            var lessonListDto = _mapper.Map<List<LessonDto>>(lessonList);

            var values = lessonListDto.Where(x => x.ClassId == id).ToList();
            return Json(values);
        }
        public async Task<JsonResult> Subject(int id)
        {
            var subjectList = await _subjectService.GetWithLessonList();
            var values = subjectList.Where(x => x.LessonId == id).ToList();
            return Json(values);
        }


        public async Task<JsonResult> Output(int id)
        {
            var outputList = await _outputService.GetAllAsycn();
            var values = outputList.Where(x => x.SubjectId == id).ToList();
            return Json(values);
        }

        public IActionResult CashCading()
        {
            return View();
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View(new QuestionDto()
            {

            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(QuestionDto model, IFormFile? file)
        {

            var questionOptions = new List<Option>(); // yeni bir liste oluştur

            foreach (var item in model.Options)
            {
                var optionValue = new Option()
                {
                    Name = item.Name,
                    Text = item.Text,
                    Condition = item.Condition,
                };
                questionOptions.Add(optionValue); // Option nesnelerini yeni liste olarak ekle
            }

            // Question nesnesini oluştur
            var values = new Question()
            {
                Text = model.Text,
                QuestionText = model.QuestionText,
                ImageUrl = file != null ? file.FileName : null,
                LessonId = model.LessonId,
                LevelId = model.LevelId,
                SubjectId = model.SubjectId,
                Options = questionOptions, // yeni liste olarak ekle
                OutputId = model.OutputId,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
            };

            // Question nesnesini veritabanına ekle
            //soru resmini wwwroot altındaki question dosyasına kaydet
            if (file != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Template\\questions", file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }


            if (values != null)
            {
               await _questionService.AddAsycn(values);
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Başarılı",
                    Message = "Soru başarıyla eklendi",
                    Css = "success"
                });
                return RedirectToAction("Index", "Question");
            }

            // Question nesnesinin Id özelliğini al
            var questionId = values.Id;

            // Option nesnelerinin QuestionId özelliğini güncelle
            foreach (var item in questionOptions)
            {
                item.QuestionId = questionId;
               await _optionService.AddAsycn(item);
            }

            //eger bir validation ile karsilasirsa dropdownlarin tekara dolmasi icin tekrar ediyoruz

            var lesson =await _lessonService.GetAllAsycn();
            ViewBag.lessons = new SelectList(lesson, "Id", "Name");

            var level =await _levelService.GetAllAsycn();
            ViewBag.levels = new SelectList(level, "Id", "Name");

            var subject =await _subjectService.GetAllAsycn();
            ViewBag.subjects = new SelectList(subject, "Id", "Name");

            var output =await _outputService.GetAllAsycn();
            ViewBag.outputs = new SelectList(output, "Id", "Name");

            var option =await _optionService.GetAllAsycn();
            ViewBag.options = new SelectList(option, "Id", "Name");

            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Soru ekleme işlemi başarısız.Lütfen bilgileri gözden geçiriniz",
                Css = "error"
            });

            return RedirectToAction("Index", "Question", model);
        }

        [HttpPost]
        public IActionResult Delete(int questionId, int outputId, int optionId, int subjectId, int lessonId)
        {
            _questionService.DeleteFromQuestion(questionId, outputId, optionId, subjectId, lessonId);
            return RedirectToAction("Index", "Question");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var values =await _questionService.GetByIdAsycn(id);
            if (values == null)
            {
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Hata",
                    Message = "Soru bulunamadı.",
                    Css = "error"
                });
            }

            var optionsList = await _optionService.GetAllAsycn();
            var options = optionsList.Where(x => x.QuestionId == id);

            return View(new QuestionDto()
            {
                Id = id,
                Text = values.Text,
                QuestionText = values.QuestionText,
                ImageUrl = values.ImageUrl,
                LessonId = values.LessonId,
                LevelId = values.LevelId,
                SubjectId = values.SubjectId,
                Options = options.ToList(),
                OutputId = values.OutputId,
                CreatedDate = values.CreatedDate,
                UpdatedDate = values.UpdatedDate,
                Condition = (bool)values.Condition,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(QuestionDto model, IFormFile file)
        {

            var values =await _questionService.GetByIdAsycn(model.Id);
            if (values != null)
            {
                values.Text = model.Text;
                values.QuestionText = model.QuestionText;
                if (file != null)
                {
                    values.ImageUrl = file.FileName;

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Template\\questions", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    };

                }
                values.LessonId = model.LessonId;
                values.LevelId = model.LevelId;
                values.SubjectId = model.SubjectId;
                values.Options = model.Options;
                values.OutputId = model.OutputId;
                values.UpdatedDate = model.UpdatedDate;
                values.Condition = model.Condition;


               await _questionService.UpdateAsycn(values);
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Başarılı",
                    Message = "Soru güncelleme işlemi başarılı",
                    Css = "success"
                });
                return RedirectToAction("Index", "Question");
            }
            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Soru güncelleme işlemi başarısız",
                Css = "error"
            });
            return View(model);

        }
    }
}
