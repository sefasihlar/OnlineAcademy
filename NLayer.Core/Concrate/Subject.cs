using NLayer.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Concrate
{
    public class Subject:BaseEntity
    {
        public string Name { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }



        public List<Question> Questions { get; set; }
    }
}
