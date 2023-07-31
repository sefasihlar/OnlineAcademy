using NLayer.Core.Abstract;

namespace NLayer.Core.Concrate
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }



        public List<Question> Questions { get; set; }
    }
}
