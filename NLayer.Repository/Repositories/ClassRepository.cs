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
    public class ClassRepository : GenericRepositoy<Class>, IClassRepository
    {
        public ClassRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Class> GetByIdWithBrances(int id)
        {
            return  await _context.Classes
                   .Where(x => x.Id == id)
                   .Include(x => x.ClassBranches)
                   .ThenInclude(x => x.Branch)
                   .FirstOrDefaultAsync();
        }

        public async Task<List<ClassBranch>> GetClassBranchList()
        {
            return  await _context.ClassBranches.ToListAsync();
        }

        public void Update(Class entity, int[] branchIds)
        {
            var _class = _context.Classes
                   .Include(x => x.ClassBranches)
                   .FirstOrDefault(x => x.Id == entity.Id);

            if (_class != null)
            {
                _class.Name = entity.Name;
                _class.UpdatedDate = entity.UpdatedDate;
                _class.Condition = entity.Condition;
                _class.ClassBranches = branchIds.Select(x => new ClassBranch()
                {
                    BranchId = x,
                    ClassId = entity.Id,
                }).ToList();

                _context.SaveChanges();

            }
        }
    }
}
