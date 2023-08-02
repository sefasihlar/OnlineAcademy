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
    public class OutputRepository : GenericRepositoy<Output>, IOutputRepository
    {
        public OutputRepository(AppDbContext context) : base(context)
        {
        }

        public void Delete(int outputId, int subjectId)
        {
            var cmd = @"delete from Outputs where Id=@p0 And SubjectId=@p1";
            _context.Database.ExecuteSqlRaw(cmd, outputId, subjectId);
        }

        public async Task<List<Output>> GetWithSubjectList()
        {
            return await  _context.Outputs
                 .Include(x => x.Subject)
                 .ToListAsync();
        }
    }
}
