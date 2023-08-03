using NLayer.Core.Concrate;

namespace NLayer.Core.DTOs.ClassBranchDtos
{
    public class ClassBranchDto
    {
        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
