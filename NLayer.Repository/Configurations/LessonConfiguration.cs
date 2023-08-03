using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Concrate;

namespace NLayer.Repository.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasOne(q => q.Class)
                 .WithMany()
                 .HasForeignKey(q => q.ClassId)
                 .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
