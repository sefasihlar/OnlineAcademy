using NLayer.Core.Abstract;
using NLayer.Core.Concrate;

namespace NLayer.Core.DTOs.OptionDtos
{
    public class OptionDto : BaseDto
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
