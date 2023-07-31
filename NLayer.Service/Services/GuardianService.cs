using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class GuardianService : Service<Guardian>
    {
        public GuardianService(IGenericRepository<Guardian> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
