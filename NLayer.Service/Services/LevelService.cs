using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class LevelService : Service<Level>,ILevelService
    {
        public LevelService(IGenericRepository<Level> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
