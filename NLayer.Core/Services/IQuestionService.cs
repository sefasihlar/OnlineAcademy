using NLayer.Core.Concrate;
using NLayer.Core.DTOs.QuestionDtos;
using NLayer.Core.GenericService;

namespace NLayer.Core.Services
{
    public interface IQuestionService : IGenericService<Question>
    {
        void UpdateAsync(QuestionDto question);

        void DeleteFromQuestion(int questionId, int outputId, int optionId, int subjectId, int lessonId);
        Task<List<QuestionDto>> GetWithList();

        Task<List<QuestionDto>> GetQuestionsByExamList(int id);
    }
}
