using AutoMapper;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ScorsDtos;
using NLayer.Core.GenericRepositories;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class ScorsService : Service<Scors>,IScorsService
    {

        private readonly IScorsRepository _scorsRepository;
        private readonly IMapper _mapper;

        public ScorsService(IGenericRepository<Scors> repository, IUnitOfWork unitOfWork, IScorsRepository scorsRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _scorsRepository=scorsRepository;
            _mapper=mapper;
        }

        public async Task<List<ScorListDto>> GetTogetherList()
        {
            var scors = await _scorsRepository.GetTogetherList();
            var scorsDto = _mapper.Map<List<ScorListDto>>(scors);
            return scorsDto.ToList();

        }
    }
}
