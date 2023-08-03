using Microsoft.EntityFrameworkCore;
using NLayer.Core.Concrate;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class SubjectRepository : GenericRepositoy<Subject>, ISubjectRepository
    {
        public SubjectRepository(AppDbContext context) : base(context)
        {
        }

        public void DeleteFromSubject(int subjectId, int lessonId)
        {
            var cmd = @"delete from Subjects where Id=@p0 And LessonId=@p1";
            _context.Database.ExecuteSqlRaw(cmd, subjectId, lessonId);
        }

        public async Task<List<Subject>> GetWithLessonList()
        {
            return await _context.Subjects.Include(x => x.Lesson).ToListAsync();
        }

        public void UpdateAsync(Subject entity)
        {
            _context.Subjects.Update(entity);
            _context.SaveChanges();
        }
    }
}
