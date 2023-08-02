using Microsoft.EntityFrameworkCore;
using NLayer.Core.Concrate;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
