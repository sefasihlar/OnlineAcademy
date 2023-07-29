using NLayer.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Concrate
{
    public class CartItem:BaseEntity
    {
        public Cart Cart { get; set; }
        public int CartId { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }
    }
}
