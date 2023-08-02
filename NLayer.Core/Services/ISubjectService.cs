using NLayer.Core.Concrate;
using NLayer.Core.DTOs.SubjectDtos;
using NLayer.Core.GenericService;

namespace NLayer.Core.Services
{
    public interface ISubjectService : IGenericService<Subject>
    {
        void DeleteFromSubject(int subjectId, int lessonId);
        Task<List<SubjectDto>> GetWithLessonList();
        void UpdateAsync(SubjectDto dto);
    }
}
