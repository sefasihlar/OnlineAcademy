using NLayer.Core.Concrate;
using NLayer.Core.DTOs.QuestionDtos;
using NLayer.Core.GenericRepositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class QuestionService : Service<Question>, IQuestionService
    {
        public QuestionService(IGenericRepository<Question> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public void DeleteFromQuestion(int questionId, int outputId, int optionId, int subjectId, int lessonId)
        {
            throw new NotImplementedException();
        }

        public Task<List<QuestionDto>> GetQuestionsByExamList(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<QuestionDto>> GetWithList()
        {
            throw new NotImplementedException();
        }

        public void UpdateAsync(QuestionDto question)
        {
            throw new NotImplementedException();
        }
    }
}
