using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;

namespace NLayer.Core.Repositories
{
    public interface ISolutionRepository : IGenericRepository<Solution>
    {
        Task<List<Solution>> GetWithQuestionList();
        void UpdateAsync(Solution entity);
        Task<Solution> GetByQuestionId(int questionId);
    }
}
