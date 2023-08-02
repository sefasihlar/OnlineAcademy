using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class ClassBranchService : Service<ClassBranch>,IClassBranchService
    {
        public ClassBranchService(IGenericRepository<ClassBranch> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
