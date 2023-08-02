using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;

namespace NLayer.Core.Repositories
{
    public interface IGuardianRepository : IGenericRepository<Guardian>
    {
        Task<List<Guardian>> GetWithStudentList();
    }
}
