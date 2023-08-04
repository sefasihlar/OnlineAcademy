using NLayer.Core.Concrate;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class AppRoleRepository : GenericRepositoy<AppRole>, IAppRoleRepository
    {
        public AppRoleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
