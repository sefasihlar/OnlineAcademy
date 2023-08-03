using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Concrate;
using NLayer.Core.DTOs.CartDtos;
using NLayer.Core.DTOs.CartItemDtos;
using NLayer.Core.Services;

namespace NLayer.Teacher.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _cartService=cartService;
            _mapper=mapper;
            _userManager=userManager;
        }

        public async Task<IActionResult> Index()
        {
            var cart = await _cartService.GetCartByUserId(_userManager.GetUserId(User));

            var cartDto = new CartDto()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(x => new CartItemDto()
                {
                    CartItemId = x.Id,
                    ExamId = x.ExamId,
                    Name = x.Exam.Title,
                }).ToList(),
            };

            return View(cartDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int examId)
        {
            var userId = _userManager.GetUserId(User);
            _cartService.AddToCart(userId, examId);
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public IActionResult DeleteFromCart(int examId)
        {
            _cartService.DeleteFromCart(_userManager.GetUserId(User), examId);
            return RedirectToAction("Index", "Cart");

        }

        public void ClearCart(string cartId)
        {
            _cartService.ClearCart(cartId);
        }
    }
}
