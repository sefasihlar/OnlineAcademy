using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;

namespace NLayer.Core.Repositories
{
    public interface IExamRepository : IGenericRepository<Exam>
    {
        Task<List<Exam>> GetWithList();

        void UpdateAsycn(Exam entity);

        void DeleteFromExam(int examId, int classId, int lessonId, int subjectId);
    }
}
