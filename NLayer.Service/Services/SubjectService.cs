using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class SubjectService : Service<Subject>
    {
        public SubjectService(IGenericRepository<Subject> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
