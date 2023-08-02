using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;

namespace NLayer.Core.Repositories
{
    public interface ISubjectRepository : IGenericRepository<Subject>
    {
        void DeleteFromSubject(int subjectId, int lessonId);
        Task<List<Subject>> GetWithLessonList();
        void UpdateAsync(Subject entity);
    }
}
