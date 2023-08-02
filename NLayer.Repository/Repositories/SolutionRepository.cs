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
    public class SolutionRepository : GenericRepositoy<Solution>, ISolutionRepository
    {
        public SolutionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Solution> GetByQuestionId(int questionId)
        {
            return await _context.Solutions
                    .Include(x => x.Question)
                    .FirstOrDefaultAsync(x => x.QuestionId == questionId);
        }

        public async Task<List<Solution>> GetWithQuestionList()
        {
            return await _context.Solutions
                    .Include(x => x.Question)
                    .Include(x => x.Option)
                    .ToListAsync();
        }

        public void UpdateAsync(Solution entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }
    }
}
