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
    public class ExamRepository : GenericRepositoy<Exam>, IExamRepository
    {
        public ExamRepository(AppDbContext context) : base(context)
        {
        }



        public void DeleteFromExam(int examId, int classId, int lessonId, int subjectId)
        {
            var cmd = @"delete from Exams where Id=@p0 And ClassId=@p1 And LessonId=@p2 And SubjectId=@p3";
            _context.Database.ExecuteSqlRaw(cmd, examId, classId, lessonId, subjectId);
        }

        public async Task<List<Exam>> GetWithList()
        {
            return await _context.Exams
                  .Include(x => x.Lesson)
                  .Include(x => x.Class)
                  .Include(x => x.Subject)
                  .Include(a => a.User)
                  .ToListAsync();
        }

        public void UpdateAsycn(Exam entity)
        {
            _context.Exams.Update(entity);
            _context.SaveChanges();
        }
    }
}
