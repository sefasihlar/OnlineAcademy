using NLayer.Core.DTOs.CartItemDtos;

namespace NLayer.Core.DTOs.CartDtos
{
    public class CartDto
    {
        public int CartId { get; set; }
        public List<CartItemDto>? CartItems { get; set; }
    }
}
