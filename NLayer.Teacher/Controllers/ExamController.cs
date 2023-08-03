using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ExamDtos;
using NLayer.Core.DTOs.MessageDtos;
using NLayer.Core.DTOs.QuestionDtos;
using NLayer.Core.DTOs.ScorsDtos;
using NLayer.Core.Services;
using NLayer.WebUI.Extensions;

namespace NLayer.Teacher.Controllers
{
    public class ExamController : Controller
    {
        private readonly IClassService _classService;
        private readonly ILessonService _lessonService;
        private readonly ISubjectService _subjectService;
        private readonly IExamService _examService;
        private readonly IScorsService _scorsService;
        private readonly IQuestionService _questionService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public ExamController(IClassService classService, ILessonService lessonService, ISubjectService subjectService, IExamService examService, IScorsService scorsService, IQuestionService questionService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _classService=classService;
            _lessonService=lessonService;
            _subjectService=subjectService;
            _examService=examService;
            _scorsService=scorsService;
            _questionService=questionService;
            _userManager=userManager;
            _mapper=mapper;
        }

        public async Task<IActionResult> Index()
        {
            //mapping işlemlerini service katmanında yaptık özel method oldugu için
            var values = await _examService.GetWithList();
            return View(values);
        }

        public async Task<IActionResult> AllExamList(int ClassId, int LessonId, int SubjectId)
        {
            var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);
            var getId = userId;

            var scorses = _scorsService.GetAllAsycn();
            ViewBag.Scors = scorses;


            var ExamList = await _examService.GetWithList();

            var ExamListDto = _mapper.Map<List<Exam>>(ExamList);

            var ScorsList = await _scorsService.GetAllAsycn();
            var ScorsListDto = _mapper.Map<ScorListDto>(ScorsList);

            List<int> examIds = new List<int>();

            if (ClassId != 0 && LessonId != 0 && SubjectId != 0)
            {


                var val = new ExamListDto()
                {
                    Exams = ExamListDto
                    .Where(a => a.ClassId == ClassId)
                    .Where(a => a.LessonId == LessonId)
                    .Where(a => a.SubjectId == SubjectId)
                    .ToList(),
                    //bruadaki scors ta hata alabilirz tür dönüşümünden dolayı
                    Scors = ScorsListDto.scors
                };

                foreach (var item in val.Exams)
                {
                    foreach (var value in val.Scors)
                    {
                        if (item.Id == value.ExamId)
                        {
                            examIds.Add(value.Id);
                        }
                    }
                }

                return View(val);
            }


            else if (ClassId != 0 && LessonId != 0 && SubjectId == 0)
            {

                ViewBag.Scors = ScorsListDto.scors;

                var val = new ExamListDto()
                {
                    Exams = ExamListDto
                      .Where(a => a.ClassId == ClassId)
                      .Where(a => a.LessonId == LessonId)
                      .ToList(),
                    Scors = ScorsListDto.scors
                };

                foreach (var item in val.Exams)
                {
                    foreach (var value in val.Scors)
                    {
                        if (item.Id == value.ExamId)
                        {
                            examIds.Add(value.Id);
                        }
                    }
                }

                return View(val);
            }


            else if (ClassId != 0 && LessonId == 0 && SubjectId == 0)
            {
                ViewBag.Scors = scorses;

                var val = new ExamListDto()
                {
                    Exams = ExamListDto
                     .Where(a => a.ClassId == ClassId)
                     .ToList(),
                    Scors = ScorsListDto.scors
                };

                foreach (var item in val.Exams)
                {
                    foreach (var value in val.Scors)
                    {
                        if (item.Id == value.ExamId)
                        {
                            examIds.Add(value.Id);
                        }
                    }
                }

                return View(val);
            }

            else
            {

                ViewBag.Scors = scorses;
            }

            var values = new ExamListDto()
            {
                Exams = ExamListDto
                 .ToList(),
                Scors = ScorsListDto.scors
            };

            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ExamDto examDto)
        {
            var userId = _userManager.GetUserId((System.Security.Claims.ClaimsPrincipal)User);

            var user = Convert.ToInt32(userId);

            var values = _mapper.Map<Exam>(examDto);
            values.UserId = user;


            if (values != null)
            {
                await _examService.AddAsycn(values);
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Basariyla Eklendi",
                    Message = "Sinav basariyla eklendi Sinavlarim kismindan eklemis oldugunuz sinavlari goruntuleyebilirsiniz",
                    Css = "success"
                });
                return RedirectToAction("Index", "Exam");

            }

            var lesson = await _lessonService.GetAllAsycn();
            ViewBag.lessons = new SelectList(lesson, "Id", "Name");

            var level = await _classService.GetAllAsycn();
            ViewBag.levels = new SelectList(level, "Id", "Name");

            var subject = await _subjectService.GetAllAsycn();
            ViewBag.subjects = new SelectList(subject, "Id", "Name");

            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Hata",
                Message = "Sinav ekleme işlemi başarısız.Lütfen bilgileri gözden geçiriniz",
                Css = "error"
            });

            return RedirectToAction("Index", "Exam", examDto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int examId, int classId, int lessonId, int subjectId)
        {
            if (examId != null || classId != null || lessonId != null || subjectId != null)
            {
                _examService.DeleteFromExam(examId, classId, lessonId, subjectId);
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Silme basarili",
                    Message = "Sinaviniz basariyla silindi",
                    Css = "success"
                });
                return RedirectToAction("Index", "Exam");
            }

            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Silme işlemli başarısız",
                Message = "Sınav silmek işlemi başarısız.Lütfen daha sonra tekrar deneyiniz.",
                Css = "erorr"
            });
            return RedirectToAction("Index", "Exam");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var values = await _examService.GetByIdAsycn(id);
            if (values == null)
            {
                return NotFound();
            }

            var valuesDto = _mapper.Map<Exam>(values);

            return View(valuesDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ExamDto examDto)
        {
            var values = await _examService.GetByIdAsycn(examDto.Id);
            if (values != null)
            {
                values.Id = examDto.Id;
                values.Title = examDto.Title;
                values.Description = examDto.Description;
                values.ClassId = examDto.ClassId;
                values.LessonId = examDto.LessonId;
                values.SubjectId = examDto.SubjectId;
                values.UpdatedDate = examDto.UpdatedDate;
                values.Condition = examDto.Condition;

                await _examService.UpdateAsycn(values);
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Guncelleme basarili",
                    Message = "Sinav guncelleme islemi basariyla gerceklesti",
                    Css = "success"
                });
                return RedirectToAction("Index", "Exam");
            }
            TempData.Put("message", new ResultMessageDto()
            {
                Title = "Guncelleme basarisiz",
                Message = "Sinav guncelleme islemi basarisiz lütfen bilgileri gozden geciriniz",
                Css = "error"
            });
            return View(examDto);
        }


        [HttpGet]
        public async Task<IActionResult> Exam(int id)
        {
            var exam = await _examService.GetByIdAsycn(id);

            var Examvalues = await _questionService.GetQuestionsByExamList(id);
            var ExamValueDto = _mapper.Map<List<Question>>(Examvalues);

            var values = new QuestionListDto()
            {

                Questions = ExamValueDto,

                SureDegeri = exam.Timer

            };

            ViewBag.ExamId = id;

            return View(values);
        }

    }
}
