using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Concrate
{
    public class ExamAnswers
    {
        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public int UserId { get; set; }
        public AppUser User { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int? OptionId { get; set; }
        public Option Option { get; set; }
    }
}
