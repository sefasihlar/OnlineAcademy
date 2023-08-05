using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.TotalCountDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.ViewComponents
{
    public class TotalQuestionViewComponent:ViewComponent
    {
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public TotalQuestionViewComponent(IQuestionService questionService, IMapper mapper)
        {
            _questionService=questionService;
            _mapper=mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var questionsList = await _questionService.GetAllAsycn();

            var values = new TotalCountsDto()
            {
                TotalQuestion = questionsList.Count()
            };
            return View(values);
        }
    }
}
