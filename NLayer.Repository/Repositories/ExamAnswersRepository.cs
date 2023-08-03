using Microsoft.EntityFrameworkCore;
using NLayer.Core.Concrate;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class ExamAnswersRepository : GenericRepositoy<ExamAnswers>, IExamAnswersRepository
    {
        public ExamAnswersRepository(AppDbContext context) : base(context)
        {
        }

        public void Create(ExamAnswers entity, int questionId, int? optionIds)
        {
            entity.QuestionId = questionId;
            entity.OptionId = optionIds;
            // "questionId" değeri "entity" nesnesinin "QuestionId" özelliğine atanır.
            _context.ExamAnswers.Add(entity);
            _context.SaveChanges();
        }

        public async Task<List<ExamAnswers>> GetListTogether()
        {
            return await _context.ExamAnswers
                    .Include(x => x.User)
                    .Include(x => x.Exam)
                    .Include(x => x.Option)
                    .Include(x => x.Question)
                    .ThenInclude(x => x.Options)
                    .ToListAsync();
        }
    }
}
