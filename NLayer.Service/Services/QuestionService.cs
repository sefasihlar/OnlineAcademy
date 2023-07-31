using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class QuestionService : Service<Question>
    {
        public QuestionService(IGenericRepository<Question> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
