using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ClassBranchDtos;
using NLayer.Core.DTOs.ClassDtos;
using NLayer.Core.GenericService;

namespace NLayer.Core.Services
{
    public interface IClassService : IGenericService<Class>
    {
        Task<ClassDto> GetByIdWithBrances(int id);
        Task<List<ClassBranchDto>> GetClassBranchList();
        void Update(ClassDto dto, int[] branchIds);
    }
}
