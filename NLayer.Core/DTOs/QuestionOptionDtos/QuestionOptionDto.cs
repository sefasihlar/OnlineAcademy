using NLayer.Core.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.QuestionOptionDtos
{
    public class QuestionOptionDto
    {
        public List<Class> Classes { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<Output> Outputs { get; set; }
    }
}
