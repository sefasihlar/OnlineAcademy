using AutoMapper;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.OutputDtos;
using NLayer.Core.GenericRepositories;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class OutputService : Service<Output>, IOutputService
    {
        private readonly IOutputRepository _outputRepository;
        private readonly IMapper _mapper;
        public OutputService(IGenericRepository<Output> repository, IUnitOfWork unitOfWork, IOutputRepository outputRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _outputRepository=outputRepository;
            _mapper=mapper;
        }

        public void Delete(int outputId, int subjectId)
        {
            _outputRepository.Delete(outputId, subjectId);
        }

        public async Task<List<OutputDto>> GetWithSubjectList()
        {
            var outputs = await _outputRepository.GetWithSubjectList();
            var outputdto = _mapper.Map<List<OutputDto>>(outputs);
            return outputdto.ToList();
        }
    }
}
