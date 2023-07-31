using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class ClassService : Service<Class>, IClassService
    {
        public ClassService(IGenericRepository<Class> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
