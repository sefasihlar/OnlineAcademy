using NLayer.Core.Concrate;
using NLayer.Core.GenericRepositories;

namespace NLayer.Core.Repositories
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        void ClearCart(string cartId);
        void DeleteFromCart(int cartId, int examId);
        Task<Cart> GetByUserId(string userId);
        Task<List<Cart>> GetListCartItem();
    }
}
