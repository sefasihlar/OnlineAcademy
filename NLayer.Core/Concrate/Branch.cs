using NLayer.Core.Abstract;

namespace NLayer.Core.Concrate
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; }

        public List<ClassBranch> ClassBranches { get; set; }
    }
}
