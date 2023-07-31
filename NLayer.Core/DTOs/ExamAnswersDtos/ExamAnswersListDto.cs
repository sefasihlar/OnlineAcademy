using NLayer.Core.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ExamAnswersDtos
{
    public class ExamAnswersListDto
    {
        public List<ExamAnswers> ExamAnswers { get; set; }

        public int UserId { get; set; }
        public int ExamId { get; set; }

        public int Score { get; set; }
        public int QuestionFalse { get; set; }
        public int QuestionTrue { get; set; }

        public int QuestionNull { get; set; }

        public List<String> CorrectAnswers { get; set; }
    }
}
