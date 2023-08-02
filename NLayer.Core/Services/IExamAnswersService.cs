using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ExamAnswersDtos;
using NLayer.Core.GenericService;

namespace NLayer.Core.Services
{
    public interface IExamAnswersService : IGenericService<ExamAnswers>
    {
        Task<List<ExamAnswersDto>> GetListTogether();
        void Create(ExamAnswersDto dto, int questionId, int? optionIds);
    }
}
