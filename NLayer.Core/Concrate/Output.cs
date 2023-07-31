using NLayer.Core.Abstract;

namespace NLayer.Core.Concrate
{
    public class Output : BaseEntity
    {
        public string Name { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

    }
}
