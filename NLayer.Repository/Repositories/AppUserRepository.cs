using Microsoft.EntityFrameworkCore;
using NLayer.Core.Concrate;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class AppUserRepository : GenericRepositoy<AppUser>, IAppUserRepository
    {
        public AppUserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<AppUser>> ListTogether()
        {
            return await _context.Users
                    .Include(x => x.Branch)
                    .Include(x => x.Class)
                    .ToListAsync();
        }
    }
}
