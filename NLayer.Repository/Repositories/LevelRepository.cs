using NLayer.Core.Concrate;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class LevelRepository : GenericRepositoy<Level>, ILevelRepository
    {
        public LevelRepository(AppDbContext context) : base(context)
        {
        }
    }
}
