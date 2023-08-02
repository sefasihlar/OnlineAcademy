using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;

namespace NLayer.Core.Repositories
{
    public interface IScorsRepository : IGenericRepository<Scors>
    {
        Task<List<Scors>> GetTogetherList();
    }
}
