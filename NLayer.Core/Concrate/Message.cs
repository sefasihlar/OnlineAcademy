using NLayer.Core.Abstract;

namespace NLayer.Core.Concrate
{
    public class Message : BaseEntity
    {
        public string? Title { get; set; }
        public string? Text { get; set; }
    }
}
