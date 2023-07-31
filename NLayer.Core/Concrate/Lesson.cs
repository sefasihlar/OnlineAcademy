using NLayer.Core.Abstract;

namespace NLayer.Core.Concrate
{
    public class Lesson : BaseEntity
    {
        public String? Name { get; set; }

        public int ClassId { get; set; }
        public virtual Class Class { get; set; }
    }
}
