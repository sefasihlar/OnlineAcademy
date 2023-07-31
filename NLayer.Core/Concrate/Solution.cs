using NLayer.Core.Abstract;

namespace NLayer.Core.Concrate
{
    public class Solution : BaseEntity
    {
        public string Text { get; set; }
        public string VideoUrl { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int OptionId { get; set; }
        public Option Option { get; set; }
    }
}
