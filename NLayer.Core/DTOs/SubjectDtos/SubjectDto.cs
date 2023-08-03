using NLayer.Core.Abstract;
using NLayer.Core.Concrate;

namespace NLayer.Core.DTOs.SubjectDtos
{
    public class SubjectDto : BaseDto
    {
        public string Name { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public bool Condition { get; set; }

        public List<Question> Questions { get; set; }
    }
}
