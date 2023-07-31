using NLayer.Core.Abstract;

namespace NLayer.Core.Concrate
{
    public class CartItem : BaseEntity
    {
        public Cart Cart { get; set; }
        public int CartId { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }
    }
}
