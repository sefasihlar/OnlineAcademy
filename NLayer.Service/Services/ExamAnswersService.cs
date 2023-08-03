using AutoMapper;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ExamAnswersDtos;
using NLayer.Core.GenericRepositories;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class ExamAnswersService : Service<ExamAnswers>, IExamAnswersService
    {
        private readonly IExamAnswersRepository _examAnswersRepository;
        private readonly IMapper _mapper;
        public ExamAnswersService(IGenericRepository<ExamAnswers> repository, IUnitOfWork unitOfWork, IExamAnswersRepository examAnswersRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _examAnswersRepository=examAnswersRepository;
            _mapper=mapper;
        }

        public void Create(ExamAnswersDto dto, int questionId, int? optionIds)
        {
            var examAnswersDto = _mapper.Map<ExamAnswers>(dto);

            _examAnswersRepository.Create(examAnswersDto, questionId, optionIds);
        }

        public async Task<List<ExamAnswersDto>> GetListTogether()
        {
            var ExamAnswers = await _examAnswersRepository.GetListTogether();
            var examAnswersDto = _mapper.Map<List<ExamAnswersDto>>(ExamAnswers);
            return examAnswersDto.ToList();

        }
    }
}
