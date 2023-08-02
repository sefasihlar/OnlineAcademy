using NLayer.Core.Concrate;
using NLayer.Core.DTOs.GuardianDtos;
using NLayer.Core.GenericService;

namespace NLayer.Core.Services
{
    public interface IGuardianService : IGenericService<Guardian>
    {
        Task<List<GuardianDto>> GetWithStudentList();
    }
}
