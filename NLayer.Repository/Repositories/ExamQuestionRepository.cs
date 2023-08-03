using Microsoft.EntityFrameworkCore;
using NLayer.Core.Concrate;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class ExamQuestionRepository : GenericRepositoy<ExamQuestions>, IExamQuestionsRepository
    {
        public ExamQuestionRepository(AppDbContext context) : base(context)
        {
        }

        public void Create(ExamQuestions entity, int questionId)
        {
            entity.QuestionId = questionId; // "questionId" değeri "entity" nesnesinin "QuestionId" özelliğine atanır.
            _context.ExamQuestions.Add(entity);
            _context.SaveChanges();
        }

        public void DeleteFromExamQuestion(ExamQuestions entity, int questionId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ExamQuestions>> GetQuestionsList()
        {
            return await _context.ExamQuestions
                    .Include(x => x.Question)
                    .ThenInclude(x => x.Options)
                    .Include(x => x.Exam)
                    .ToListAsync();
        }

        public void Update(ExamQuestions entity, int[] questionIds)
        {
            var questiton = _context.ExamQuestions
                    .Where(x => x.ExamId == entity.ExamId);
            if (questiton != null)
            {
                questiton.Select(x => new ExamQuestions()
                {
                    ExamId = entity.ExamId,
                    QuestionId = x.QuestionId
                });
            }

            _context.SaveChanges();
        }
    }
}
