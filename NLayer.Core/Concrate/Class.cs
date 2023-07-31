using NLayer.Core.Abstract;

namespace NLayer.Core.Concrate
{
    public class Class : BaseEntity
    {
        public string? Name { get; set; }
        public List<ClassBranch> ClassBranches { get; set; }
    }
}
