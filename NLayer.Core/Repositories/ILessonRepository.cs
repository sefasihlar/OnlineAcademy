using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;

namespace NLayer.Core.Repositories
{
    public interface ILessonRepository : IGenericRepository<Lesson>
    {
        void UpdateAsycn(Lesson entity);
        void DeleteFromLesson(int lessonId, int classId);
        Task<List<Lesson>> GetWithClassList();
    }
}
