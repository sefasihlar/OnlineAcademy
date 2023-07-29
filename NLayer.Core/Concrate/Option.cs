using NLayer.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Concrate
{
    public class Option:BaseEntity
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public List<ExamAnswers> ExamAnswers { get; set; }
    }
}
