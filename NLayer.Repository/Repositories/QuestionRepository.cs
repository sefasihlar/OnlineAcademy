using Microsoft.EntityFrameworkCore;
using NLayer.Core.Concrate;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class QuestionRepository : GenericRepositoy<Question>, IQuestionRepository
    {
        public QuestionRepository(AppDbContext context) : base(context)
        {
        }

        public void DeleteFromQuestion(int questionId, int outputId, int optionId, int subjectId, int lessonId)
        {
            var cmd = @"delete from Question where QuestionId=@p0 And OptionId=@p1 And SubjectId=@p2 And LessonId=@p3";
            _context.Database.ExecuteSqlRaw(cmd, questionId, outputId, optionId, lessonId);
        }

        public async Task<List<Question>> GetQuestionsByExamList(int id)
        {
            var questions = _context.Questions.AsQueryable();

            if (id != null)
            {
                questions = questions
                    .Include(x => x.Lesson)
                    .Include(x => x.Level)
                    .Include(x => x.Subject)
                    .Include(x => x.Output)
                    .Include(x => x.Options)
                    .Include(x => x.ExamQuestions)
                    .ThenInclude(a => a.Exam)
                    .Where(x => x.ExamQuestions.Any(x => x.Exam.Id == id));

            }

            return await questions.ToListAsync();
        }

        public async Task<List<Question>> GetWithList()
        {
            return await _context.Questions
                    .Include(x => x.Lesson)
                    .Include(x => x.Level)
                    .Include(x => x.Subject)
                    .Include(x => x.Options).ToListAsync();
        }

        public void UpdateAsync(Question entity)
        {
            _context.Questions.Update(entity);
            _context.SaveChanges();
        }
    }
}
