using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ExamQuestionDtos;
using NLayer.Core.GenericService;

namespace NLayer.Core.Services
{
    public interface IExamQuestionsService : IGenericService<ExamQuestions>
    {
        void Create(ExamQuestionDto dto, int questionId);
        void Update(ExamQuestionDto dto, int[] questionIds);
        Task<List<ExamQuestionDto>> GetQuestionsList();
        void DeleteFromExamQuestion(ExamQuestionDto dto, int questionId);
    }
}
