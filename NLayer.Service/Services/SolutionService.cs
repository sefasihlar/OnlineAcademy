using AutoMapper;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.SolutionDtos;
using NLayer.Core.GenericRepositories;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class SolutionService : Service<Solution>, ISolutionService
    {
        private readonly ISolutionRepository _solutionRepository;
        private readonly IMapper _mapper;
        public SolutionService(IGenericRepository<Solution> repository, IUnitOfWork unitOfWork, ISolutionRepository solutionRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _solutionRepository=solutionRepository;
            _mapper=mapper;
        }

        public async Task<SolutionDto> GetByQuestionId(int questionId)
        {
            var sulution = await _solutionRepository.GetByQuestionId(questionId);
            var solutionDto = _mapper.Map<SolutionDto>(sulution);
            return solutionDto;
        }

        public async Task<List<SolutionDto>> GetWithQuestionList()
        {
            var solutions = await _solutionRepository.GetWithQuestionList();
            var solutionDtos = _mapper.Map<List<SolutionDto>>(solutions);
            return solutionDtos.ToList();
        }

        public void UpdateAsync(SolutionDto entity)
        {
            var solution = _mapper.Map<Solution>(entity);
            _solutionRepository.UpdateAsync(solution);
        }
    }
}
