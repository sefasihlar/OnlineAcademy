using AutoMapper;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.CartDtos;
using NLayer.Core.GenericRepositories;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.GenericManager;

namespace NLayer.Service.Services
{
    public class CartService : Service<Cart>, ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartService(IGenericRepository<Cart> repository, IUnitOfWork unitOfWork, ICartRepository cartRepository, IMapper mapper = null) : base(repository, unitOfWork)
        {
            _cartRepository=cartRepository;
            _mapper=mapper;
        }

        public void AddToCart(string userId, int examId)
        {
            throw new NotImplementedException();
        }

        //public void AddToCart(string userId, int examId)
        //{
        //    var cart = GetCartByUserId(userId);
        //    if (cart != null)
        //    {
        //        var index = cart.CartItems.FindIndex(x => x.ExamId == examId);
        //        if (index < 0)
        //        {
        //            cart.CartItems.Add(new CartItem()
        //            {
        //                ExamId = examId,
        //                CartId = cart.Id,
        //            });
        //        }


        //        _cartRepository.Update(cart);
        //    }
        //}

        //burada hata alabiliriz
        public void ClearCart(string cartId)
        {
            var cart = _cartRepository.GetByIdAsycn(Convert.ToInt16(cartId));
            var cartDto = _mapper.Map<Cart>(cart);
            _cartRepository.Remove(cartDto);
        }

      
        public void DeleteFromCart(string userId, int examId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
               _cartRepository.DeleteFromCart(cart.Id, examId);
            }
        }

        public void DeleteFromCart(int cartId, int examId)
        {
            throw new NotImplementedException();
        }

        public async Task<CartDto> GetByUserId(string userId)
        {
            var cart = await _cartRepository.GetByUserId(userId);
            var cartDto = _mapper.Map<CartDto>(cart);
            return cartDto;
        }

        public async Task<Cart> GetCartByUserId(string userId)
        {
            return await _cartRepository.GetByUserId(userId);
        }

        public async Task<List<CartDto>> GetListCartItem()
        {
            var cars =  _cartRepository.GetAll();
            var cartDto  = _mapper.Map<List<CartDto>>(cars);
            return cartDto;
        }

        public void InitializeCart(string userId)
        {
            _cartRepository.AddAsycn(new Cart() { UserId = userId });
        }

        Cart ICartService.GetCartByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
