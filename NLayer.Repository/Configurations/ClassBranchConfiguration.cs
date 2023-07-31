using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Concrate;
using System.Reflection.Emit;

namespace NLayer.Repository.Configurations
{
    public class ClassBranchConfiguration : IEntityTypeConfiguration<ClassBranch>
    {
        public void Configure(EntityTypeBuilder<ClassBranch> builder)
        {
            builder.HasKey(c => new { c.ClassId, c.BranchId });
        }
    }
}
