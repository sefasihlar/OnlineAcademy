using NLayer.Core.Concrate;
using NLayer.Core.DTOs.GuardianDtos;
using NLayer.Core.GenericRepositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class GuardianService : Service<Guardian>, IGuardianService
    {
        public GuardianService(IGenericRepository<Guardian> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }

        public Task<List<GuardianDto>> GetWithStudentList()
        {
            throw new NotImplementedException();
        }
    }
}
