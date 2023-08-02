using NLayer.Core.Concrate;
using NLayer.Core.DTOs.CartDtos;
using NLayer.Core.GenericService;

namespace NLayer.Core.Services
{
    public interface ICartService : IGenericService<Cart>
    {

        void InitializeCart(string userId);
        void ClearCart(string cartId);
        void DeleteFromCart(int cartId, int examId);
        Task<CartDto> GetByUserId(string userId);
        Task<List<CartDto>> GetListCartItem();

        void DeleteFromCart(string userId, int examId);

        Task<Cart> GetCartByUserId(string userId);

        void AddToCart(string userId, int examId);

    }
}
