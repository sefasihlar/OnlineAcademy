using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;

namespace NLayer.Core.Repositories
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        void UpdateAsync(Question question);

        void DeleteFromQuestion(int questionId, int outputId, int optionId, int subjectId, int lessonId);
        Task<List<Question>> GetWithList();

        Task<List<Question>> GetQuestionsByExamList(int id);
    }
}
