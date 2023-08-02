using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ExamDtos;
using NLayer.Core.GenericService;

namespace NLayer.Core.Services
{
    public interface IExamService : IGenericService<Exam>
    {
        Task<List<ExamDto>> GetWithList();

         void UpdateAsycn(ExamDto dto);

        void DeleteFromExam(int examId, int classId, int lessonId, int subjectId);
    }
}
