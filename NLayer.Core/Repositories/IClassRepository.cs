using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;

namespace NLayer.Core.Repositories
{
    public interface IClassRepository : IGenericRepository<Class>
    {
        Task<Class> GetByIdWithBrances(int id);
        Task<List<ClassBranch>> GetClassBranchList();
        void Update(Class entity, int[] branchIds);
    }
}
