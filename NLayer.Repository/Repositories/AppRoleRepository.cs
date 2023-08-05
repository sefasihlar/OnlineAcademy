using NLayer.Core.Concrate;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class AppRoleRepository : GenericRepositoy<AppRole>, IAppRoleRepository
    {
        public AppRoleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
