using Microsoft.EntityFrameworkCore;
using NLayer.Core.Concrate;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class CartRepository : GenericRepositoy<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }

        public void ClearCart(string cartId)
        {
            var cmd = @"delete from CartItem where CartId=@p0";
            _context.Database.ExecuteSqlRaw(cmd, cartId, cartId);
        }

        public void DeleteFromCart(int cartId, int examId)
        {
            var cmd = @"delete from CartItem where CartId=@p0 And examId=@p1";
            _context.Database.ExecuteSqlRaw(cmd, cartId, examId);
        }

        public async Task<Cart> GetByUserId(string userId)
        {
            return  _context.Carts
                                    .Include(x => x.CartItems)
                                    .ThenInclude(x => x.Exam)
                                    .ThenInclude(x => x.Subject)
                                    .ThenInclude(x => x.Lesson)
                                    .ThenInclude(x => x.Class)
                                    .FirstOrDefault(x => x.UserId == userId);
        }

        public async Task<List<Cart>> GetListCartItem()
        {
            return await _context.Carts
                   .Include(x => x.CartItems).ToListAsync();
        }

    }
}
