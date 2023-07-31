using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Concrate;

namespace NLayer.Repository.Configurations
{
    public class ScorsConfiguration : IEntityTypeConfiguration<Scors>
    {
        public void Configure(EntityTypeBuilder<Scors> builder)
        {
            builder.HasOne(q => q.Exam)
              .WithMany()
              .HasForeignKey(q => q.ExamId)
              .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
