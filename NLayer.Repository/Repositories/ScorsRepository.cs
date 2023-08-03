using Microsoft.EntityFrameworkCore;
using NLayer.Core.Concrate;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class ScorsRepository : GenericRepositoy<Scors>, IScorsRepository
    {
        public ScorsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Scors>> GetTogetherList()
        {
            return await _context.Scors
                   .Include(x => x.Exam)
                   .ThenInclude(x => x.Subject)
                   .Include(x => x.Exam)
                   .ThenInclude(x => x.Lesson)
                   .Include(x => x.Exam)
                   .ThenInclude(x => x.Class)
                   .Include(a => a.Exam)
                   .ThenInclude(x => x.User)
                   .Include(a => a.User)
                   .Include(a => a.Exam)
                   .ToListAsync();
        }
    }
}
