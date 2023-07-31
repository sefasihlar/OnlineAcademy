using NLayer.Core.Abstract;

namespace NLayer.Core.Concrate
{
    public class Exam : BaseEntity
    {

        public string? Title { get; set; }
        public String Description { get; set; }

        public string? ExamDate { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public int Timer { get; set; }

        public List<ExamAnswers> ExamAnswers { get; set; }
        public List<ExamQuestions> ExamQuestions { get; set; }

    }
}
