using NLayer.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Concrate
{
    public class Lesson:BaseEntity
    {
        public String? Name { get; set; }

        public int ClassId { get; set; }
        public virtual Class Class { get; set; }
    }
}
