using AutoMapper;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ExamDtos;
using NLayer.Core.GenericRepositories;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class ExamService : Service<Exam>,IExamService
    {
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;

        public ExamService(IGenericRepository<Exam> repository, IUnitOfWork unitOfWork, IExamRepository examRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _examRepository=examRepository;
            _mapper=mapper;
        }

        public void DeleteFromExam(int examId, int classId, int lessonId, int subjectId)
        {
            _examRepository.DeleteFromExam(examId,classId, lessonId, subjectId);
        }

        public async Task<List<ExamDto>> GetWithList()
        {
            var exams = await _examRepository.GetWithList();
            var examsdto = _mapper.Map<List<ExamDto>>(exams);
            return examsdto.ToList();
        }

        public void UpdateAsycn(ExamDto dto)
        {
            var entity = _mapper.Map<Exam>(dto);
            _examRepository.UpdateAsycn(entity);
        }
    }
}
