using NLayer.Core.Concrate;
using NLayer.Core.DTOs.ScorsDtos;

namespace NLayer.Core.DTOs.ExamDtos
{
    public class ExamListDto
    {
        public List<Exam> Exams { get; set; }
        public List<ScorListDto> Scors { get; set; }
    }
}
