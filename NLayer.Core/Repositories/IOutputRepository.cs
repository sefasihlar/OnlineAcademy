using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;

namespace NLayer.Core.Repositories
{
    public interface IOutputRepository : IGenericRepository<Output>
    {
        void Delete(int outputId, int subjectId);
        Task<List<Output>> GetWithSubjectList();
    }
}
