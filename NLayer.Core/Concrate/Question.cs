using NLayer.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NLayer.Core.Concrate
{
    public class Question:BaseEntity
    {
        public string? Text { get; set; }
        public string? ImageUrl { get; set; }
        public string? QuestionText { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }


        public int LevelId { get; set; }
        public Level Level { get; set; }



        public int SubjectId { get; set; }
        public Subject Subject { get; set; }



        public int OutputId { get; set; }
        public Output Output { get; set; }


        public List<Option> Options { get; set; }
        public List<ExamAnswers> ExamAnswers { get; set; }
        public List<ExamQuestions> ExamQuestions { get; set; }


        public Boolean SolutionCondition { get; set; }
    }
}
