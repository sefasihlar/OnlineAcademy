using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Concrate;
using System.Reflection.Emit;

namespace NLayer.Repository.Configurations
{
    public class ExamQuestionConfiguration : IEntityTypeConfiguration<ExamQuestions>
    {
        public void Configure(EntityTypeBuilder<ExamQuestions> builder)
        {
            builder.HasKey(c => new { c.ExamId, c.QuestionId });
        }
    }
}
