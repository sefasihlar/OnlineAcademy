using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class SolutionService : Service<Solution>
    {
        public SolutionService(IGenericRepository<Solution> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
