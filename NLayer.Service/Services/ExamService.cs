using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class ExamService : Service<Exam>
    {
        public ExamService(IGenericRepository<Exam> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
