using NLayer.Core.Abstract;
using NLayer.Core.Concrate;

namespace NLayer.Core.DTOs.SolutionDtos
{
    public class SolutionDto : BaseDto
    {
        public string Text { get; set; }
        public string VideoUrl { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int OptionId { get; set; }
        public Option? Option { get; set; }
    }
}
