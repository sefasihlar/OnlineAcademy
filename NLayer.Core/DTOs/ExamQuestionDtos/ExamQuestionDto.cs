using NLayer.Core.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ExamQuestionDtos
{
    public class ExamQuestionDto
    {
        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }


        public List<Question> SelectedQuestions { get; set; }
    }
}
