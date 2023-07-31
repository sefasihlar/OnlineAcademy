using NLayer.Core.Abstract;
using NLayer.Core.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.QuestionDtos
{
    public class QuestionDto:BaseDto
    {
        public string? Text { get; set; }
        public string? ImageUrl { get; set; }

        public string QuestionText { get; set; }

        //public int LessonId { get; set; }
        //public Lesson Lesson { get; set; }
        public int LevelId { get; set; }
        public Level Level { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public List<Option> Options { get; set; }

        public int OutputId { get; set; }
        public Output Output { get; set; }

        public int UserId { get; set; }
        public AppUser? User { get; set; }

        public bool SolutionCondition { get; set; }
    }
}
