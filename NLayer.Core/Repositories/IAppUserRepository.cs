using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;

namespace NLayer.Core.Repositories
{
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {
        Task<List<AppUser>> ListTogether();
    }
}
