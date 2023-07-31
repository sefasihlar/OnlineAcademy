using NLayer.Core.DTOs.CartItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.CartDtos
{
    public class CartDto
    {
        public int CartId { get; set; }
        public List<CartItemDto>? CartItems { get; set; }
    }
}
