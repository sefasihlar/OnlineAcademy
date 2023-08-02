using AutoMapper;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.LessonDtos;
using NLayer.Core.GenericRepositories;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class LessonService : Service<Lesson>,ILessonService
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;
        public LessonService(IGenericRepository<Lesson> repository, IUnitOfWork unitOfWork, ILessonRepository lessonRepository, IMapper mapper) : base(repository, unitOfWork)
        {
            _lessonRepository=lessonRepository;
            _mapper=mapper;
        }

        public void DeleteFromLesson(int lessonId, int classId)
        {
            _lessonRepository.DeleteFromLesson(lessonId, classId);
        }

        public async Task<List<LessonDto>> GetWithClassList()
        {
            var Lessons = await _lessonRepository.GetWithClassList();
            var lessonDto = _mapper.Map<List<LessonDto>>(Lessons);
            return lessonDto.ToList();
        }

        void ILessonService.UpdateAsycn(LessonDto dto)
        {
            var entity = _mapper.Map<Lesson>(dto);
            _lessonRepository.UpdateAsycn(entity);
        }
    }
}
