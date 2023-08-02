using AutoMapper;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.SubjectDtos;
using NLayer.Core.GenericRepositories;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class SubjectService : Service<Subject>,ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;
        public SubjectService(IGenericRepository<Subject> repository, IUnitOfWork unitOfWork, ISubjectRepository subjectRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _subjectRepository=subjectRepository;
            _mapper=mapper;
        }

        public void DeleteFromSubject(int subjectId, int lessonId)
        {
            _subjectRepository.DeleteFromSubject(subjectId, lessonId);
        }

        public async Task<List<SubjectDto>> GetWithLessonList()
        {
            var subjects  = await _subjectRepository.GetWithLessonList();
            var subjectsDto = _mapper.Map<List<SubjectDto>>(subjects);
            return subjectsDto.ToList();
        }

        public void UpdateAsync(SubjectDto dto)
        {
           var entity  = _mapper.Map<Subject>(dto);
            _subjectRepository.UpdateAsync(entity);
        }
    }
}
