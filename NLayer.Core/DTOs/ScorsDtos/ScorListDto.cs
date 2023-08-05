using NLayer.Core.Abstract;
using NLayer.Core.Concrate;

namespace NLayer.Core.DTOs.ScorsDtos
{
    public class ScorListDto : BaseDto
    {
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public List<ScorListDto> scors { get; set; }
        public int LessonId { get; set; }

        public string? ExamTitle { get; set; }
        public string? ExamDescription { get; set; }
        public string? ExamClass { get; set; }
        public int ExamDate { get; set; }
        public int ExamTimer { get; set; }

        public decimal TotalNullPercentile { get; set; }
        public decimal TotalFalsePercentile { get; set; }
        public decimal TotalTruePercentile { get; set; }
        public decimal TotalScorPercentile { get; set; }


    }
}
