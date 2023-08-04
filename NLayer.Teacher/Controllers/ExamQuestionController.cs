using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ExamDtos;
using NLayer.Core.DTOs.ExamQuestionDtos;
using NLayer.Core.DTOs.MessageDtos;
using NLayer.Core.DTOs.QuestionDtos;
using NLayer.Core.Services;
using NLayer.WebUI.Extensions;

namespace NLayer.Teacher.Controllers
{
    public class ExamQuestionController : Controller
    {
        private readonly IExamQuestionsService _examQuestionsService;
        private readonly IExamService _examService;
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;



        public async Task<IActionResult> Index(ExamDto examDto)
        {
            List<int> SelectedQuestionIds = new List<int>();
            var ExamQuestions = await _examQuestionsService.GetQuestionsList();
            var questionFilter = ExamQuestions.Where(x => x.ExamId == examDto.Id).ToList(); 
            var questions = await _questionService.GetWithList(); 
            var questionEntity = _mapper.Map<List<Question>>(questions);

            var values = new QuestionListDto()
            {
                Questions = questionEntity
                .Where(x => x.LessonId == examDto.LessonId)
                .Where(x => x.SubjectId == examDto.SubjectId)
                .Where(a => a.Condition == true)
                .ToList(),
                SelectedQuestions = SelectedQuestionIds
            };

            foreach (var item in questionFilter)
            {
                if (values.Questions.Any(x => x.Id == item.QuestionId))
                {
                    SelectedQuestionIds.Add(item.QuestionId);
                }
            }

            if (SelectedQuestionIds.Count == 0)
            {
                ViewBag.btnCondition = false;
            }

            else
            {
                ViewBag.btnCondition = true;
            }

            ViewBag.ExamId = examDto.Id;

            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExamQuestionDto examQuestionDto, int[] questionIds)
        {
            if (examQuestionDto != null & questionIds != null)
            {

                foreach (var item in questionIds)
                {
                    //buraya userId eklenecek
                    _examQuestionsService.Create(examQuestionDto, item);

                };
                TempData.Put("message", new ResultMessageDto()
                {
                    Title = "Başarılı",
                    Message = "Sinav soruları basariyla eklendi",
                    Css = "success"
                });
                return RedirectToAction("Index", "Exam");

            }

            return View(examQuestionDto);
        }

    }
}
