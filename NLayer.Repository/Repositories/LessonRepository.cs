using Microsoft.EntityFrameworkCore;
using NLayer.Core.Concrate;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class LessonRepository : GenericRepositoy<Lesson>, ILessonRepository
    {
        public LessonRepository(AppDbContext context) : base(context)
        {
        }

        public void DeleteFromLesson(int lessonId, int classId)
        {
            var cmd = @"delete from Lessons where Id=@p0 And ClassId=@p1";
            _context.Database.ExecuteSqlRaw(cmd, lessonId, classId);
        }

        public async Task<List<Lesson>> GetWithClassList()
        {
            return await _context.Lessons
                  .Include(x => x.Class).ToListAsync();
        }

        public void UpdateAsycn(Lesson entity)
        {
            _context.Lessons.Update(entity);
            _context.SaveChanges();

        }
    }
}
