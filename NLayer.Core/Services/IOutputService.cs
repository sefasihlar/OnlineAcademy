using NLayer.Core.Concrate;
using NLayer.Core.DTOs.OutputDtos;
using NLayer.Core.GenericService;

namespace NLayer.Core.Services
{
    public interface IOutputService : IGenericService<Output>
    {
        void Delete(int outputId, int subjectId);
        Task<List<OutputDto>> GetWithSubjectList();
    }
}
