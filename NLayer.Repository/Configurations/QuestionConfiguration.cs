using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Concrate;

namespace NLayer.Repository.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasOne(q => q.Level)
                 .WithMany()
                 .HasForeignKey(q => q.LevelId)
                 .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(q => q.Output)
                 .WithMany()
                 .HasForeignKey(q => q.OutputId)
                 .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(q => q.Subject)
                .WithMany(s => s.Questions)
                .HasForeignKey(q => q.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(q => q.Lesson)
               .WithMany()
               .HasForeignKey(q => q.LessonId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
