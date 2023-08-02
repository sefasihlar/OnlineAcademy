using AutoMapper;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ExamQuestionDtos;
using NLayer.Core.GenericRepositories;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class ExamQuestionService : Service<ExamQuestions>,IExamQuestionsService
    {
        private readonly IExamQuestionsRepository _examQuestionsRepository;
        private readonly IMapper _mapper;
        public ExamQuestionService(IGenericRepository<ExamQuestions> repository, IUnitOfWork unitOfWork, IExamQuestionsRepository examQuestionsRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _examQuestionsRepository=examQuestionsRepository;
            _mapper=mapper;
        }

        public void Create(ExamQuestionDto dto, int questionId)
        {
            var entity = _mapper.Map<ExamQuestions>(dto);
            _examQuestionsRepository.Create(entity, questionId);

        }

        public void DeleteFromExamQuestion(ExamQuestionDto dto, int questionId)
        {
            var entity = _mapper.Map<ExamQuestions>(dto);
            _examQuestionsRepository.DeleteFromExamQuestion(entity, questionId);
        }

        public async Task<List<ExamQuestionDto>> GetQuestionsList()
        {
            var examQuestions = await _examQuestionsRepository.GetQuestionsList();
            var examQuestionDto = _mapper.Map<List<ExamQuestionDto>>(examQuestions);
            return examQuestionDto.ToList();
        }

        public void Update(ExamQuestionDto dto, int[] questionIds)
        {
            var entity = _mapper.Map<ExamQuestions>(dto);
            _examQuestionsRepository.Update(entity, questionIds);
        }
    }
}
