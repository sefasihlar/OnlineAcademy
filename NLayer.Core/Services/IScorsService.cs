using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ScorsDtos;
using NLayer.Core.GenericService;

namespace NLayer.Core.Services
{
    public interface IScorsService : IGenericService<Scors>
    {
        Task<List<ScorListDto>> GetTogetherList();
    }
}
