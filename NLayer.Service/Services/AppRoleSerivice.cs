using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class AppRoleSerivice : Service<AppRole>, IAppRoleService
    {
        public AppRoleSerivice(IGenericRepository<AppRole> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
