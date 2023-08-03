using NLayer.Core.Concrate;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class ClassBranchRepository : GenericRepositoy<ClassBranch>, IClassBranchRepository
    {
        public ClassBranchRepository(AppDbContext context) : base(context)
        {
        }
    }
}
