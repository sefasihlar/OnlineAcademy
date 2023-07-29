using NLayer.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Concrate
{
    public class Scors:BaseEntity
    {
        public int UserId { get; set; }
        public AppUser User { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public int True { get; set; }
        public int False { get; set; }
        public int Null { get; set; }
        public decimal Average { get; set; }
        public decimal Scor { get; set; }
    }
}
