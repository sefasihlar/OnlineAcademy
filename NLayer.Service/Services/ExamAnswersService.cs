using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class ExamAnswersService : Service<ExamAnswers>
    {
        public ExamAnswersService(IGenericRepository<ExamAnswers> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
