using NLayer.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Concrate
{
    public class Cart:BaseEntity
    {
    
        public string UserId { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
