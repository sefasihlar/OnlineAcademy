using NLayer.Core.Abstract;

namespace NLayer.Core.Concrate
{
    public class Option : BaseEntity
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public List<ExamAnswers> ExamAnswers { get; set; }
    }
}
