using NLayer.Core.Abstract;

namespace NLayer.Core.Concrate
{
    public class Cart : BaseEntity
    {

        public string UserId { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
