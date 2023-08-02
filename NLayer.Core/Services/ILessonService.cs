using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ExamDtos;
using NLayer.Core.DTOs.LessonDtos;
using NLayer.Core.GenericService;

namespace NLayer.Core.Services
{
    public interface ILessonService : IGenericService<Lesson>
    {
        void UpdateAsycn(LessonDto dto);
        void DeleteFromLesson(int lessonId, int classId);
        Task<List<LessonDto>> GetWithClassList();
    }
}
