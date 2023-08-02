using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;

namespace NLayer.Core.Repositories
{
    public interface IExamQuestionsRepository : IGenericRepository<ExamQuestions>
    {
        void Create(ExamQuestions entity, int questionId);
        void Update(ExamQuestions entity, int[] questionIds);
        Task<List<ExamQuestions>> GetQuestionsList();
        void DeleteFromExamQuestion(ExamQuestions entity, int questionId);
    }
}
