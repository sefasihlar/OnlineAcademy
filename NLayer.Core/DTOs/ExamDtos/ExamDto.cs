using NLayer.Core.Abstract;
using NLayer.Core.Concrate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ExamDtos
{
    public class ExamDto:BaseDto
    {
  
        [Required]
        public string? Title { get; set; }
        public string Description { get; set; }
        public string ExamDate { get; set; }

        public int Timer { get; set; }

        public int ClassId { get; set; }
        public Class Class { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int UserId { get; set; }
        public AppUser? User { get; set; }
        public List<Question> Questions { get; set; }
    }
}
