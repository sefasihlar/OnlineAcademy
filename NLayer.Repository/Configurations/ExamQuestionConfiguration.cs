using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configurations
{
    public class ExamQuestionConfiguration : IEntityTypeConfiguration<ExamQuestions>
    {
        public void Configure(EntityTypeBuilder<ExamQuestions> builder)
        {
            throw new NotImplementedException();
        }
    }
}
