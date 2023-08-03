using NLayer.Core.Concrate;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class OptionRepository : GenericRepositoy<Option>, IOptionRepository
    {
        public OptionRepository(AppDbContext context) : base(context)
        {
        }
    }
}
