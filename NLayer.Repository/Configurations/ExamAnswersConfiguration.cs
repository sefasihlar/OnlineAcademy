using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Concrate;
using System.Reflection.Emit;

namespace NLayer.Repository.Configurations
{
    public class ExamAnswersConfiguration : IEntityTypeConfiguration<ExamAnswers>
    {
        public void Configure(EntityTypeBuilder<ExamAnswers> builder)
        {

            builder.HasKey(e => new { e.ExamId, e.UserId, e.QuestionId });
            builder.HasOne(e => e.Option)
               .WithMany()
               .HasForeignKey(e => e.OptionId);
            // Relationships
            builder.HasOne(e => e.Exam)
                .WithMany(e => e.ExamAnswers)
                .HasForeignKey(e => e.ExamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Question)
                .WithMany(q => q.ExamAnswers)
                .HasForeignKey(e => e.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Option)
            .WithMany(o => o.ExamAnswers)
            .HasForeignKey(e => e.OptionId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(c => new { c.ExamId, c.QuestionId, c.UserId });
        }
    }
}
