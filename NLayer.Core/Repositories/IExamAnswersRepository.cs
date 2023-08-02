using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;

namespace NLayer.Core.Repositories
{
    public interface IExamAnswersRepository : IGenericRepository<ExamAnswers>
    {
        Task<List<ExamAnswers>> GetListTogether();
        void Create(ExamAnswers entity, int questionId, int? optionIds);
    }
}
