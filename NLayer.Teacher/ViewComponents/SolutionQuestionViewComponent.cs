using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.QuestionDtos;
using NLayer.Core.Repositories;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class SolutionQuestionViewComponent : ViewComponent
    {
        private readonly IQuestionService _questionService;

        public SolutionQuestionViewComponent(IQuestionService questionService)
        {
            _questionService=questionService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var valuesList = await _questionService.GetWithList();
            var values = valuesList.FirstOrDefault(x => x.Id == ViewBag.QuestionId);

            var question = new QuestionDto()
            {
                Id = values.Id,
                Text = values.Text,
                ImageUrl = values.ImageUrl,
                QuestionText = values.QuestionText,
                LessonId = values.LessonId,
                LevelId = values.LevelId,
                SubjectId = values.SubjectId,
                OutputId = values.OutputId,
                CreatedDate = values.CreatedDate,
                UpdatedDate = values.UpdatedDate,
                Condition = values.Condition,
                SolutionCondition = values.SolutionCondition,
            };

            if (question != null)
            {
                return View(question);
            }

            return View();
        }
    }
}
