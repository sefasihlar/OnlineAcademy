using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class ScorsService : Service<Scors>
    {
        public ScorsService(IGenericRepository<Scors> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
