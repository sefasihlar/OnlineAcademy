using Microsoft.EntityFrameworkCore;
using NLayer.Core.Concrate;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class GuardianRepository : GenericRepositoy<Guardian>, IGuardianRepository
    {
        public GuardianRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Guardian>> GetWithStudentList()
        {
            return await _context.Guardians
                  .Include(x => x.User)
                  .ToListAsync();
        }
    }
}
