using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class OutputService : Service<Output>
    {
        public OutputService(IGenericRepository<Output> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
