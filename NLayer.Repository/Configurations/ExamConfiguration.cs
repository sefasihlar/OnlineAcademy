using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Concrate;

namespace NLayer.Repository.Configurations
{
    public class ExamConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasOne(q => q.Class)
                  .WithMany()
                  .HasForeignKey(q => q.ClassId)
                  .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(q => q.Subject)
                .WithMany()
                .HasForeignKey(q => q.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(q => q.Lesson)
                 .WithMany()
                 .HasForeignKey(q => q.LessonId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
