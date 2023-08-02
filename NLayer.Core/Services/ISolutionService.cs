using NLayer.Core.Concrate;
using NLayer.Core.DTOs.SolutionDtos;
using NLayer.Core.GenericService;

namespace NLayer.Core.Services
{
    public interface ISolutionService : IGenericService<Solution>
    {
        Task<List<SolutionDto>> GetWithQuestionList();
        void UpdateAsync(SolutionDto entity);
        Task<SolutionDto> GetByQuestionId(int questionId);
    }
}
