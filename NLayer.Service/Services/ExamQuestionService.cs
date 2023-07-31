using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class ExamQuestionService : Service<ExamQuestions>
    {
        public ExamQuestionService(IGenericRepository<ExamQuestions> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
