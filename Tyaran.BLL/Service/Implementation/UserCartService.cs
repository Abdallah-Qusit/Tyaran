using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.BLL.Service.Abstraction;
using Tyaran.DAL.Entities.Generated;
using Tyaran.DAL.Repo.Abstraction;
using Tyaran.DAL.Repo.Implementation;

namespace Tyaran.BLL.Service.Implementation
{
    public class UserCartService : IUserCartService
    {
        private readonly IUserCartRepo _userCartRepo;
        public UserCartService(IUserCartRepo userCartRepo)
        {
            _userCartRepo = userCartRepo;
        }
        public async Task AddItemAsync(int userId, int itemId, int quantity, string specialInst)
        {
            var cart = await _userCartRepo.GetUserCartAsync(userId);
            int cartId;
            if (cart == null)
            {
                cartId = await _userCartRepo.CreateCart(userId);
            }
            else
            {
                cartId = cart.CartId;
            }
            await _userCartRepo.AddToCartAsync(specialInst,cartId,itemId,quantity);
        }

        public async Task<int> CheckoutAsync(int userId, int addressId, string paymentMethod)
        {
            var cart =await _userCartRepo.GetUserCartAsync(userId);
            if (cart == null)
                throw new Exception("Cart Empty");
            int cartId = cart.CartId;
            var cartItems = await _userCartRepo.GetCartItemsAsync(cartId);
            decimal subtotal = (decimal)cartItems.Sum(x => x.Quantity * x.Item.Price);
            decimal tax = subtotal * 0.14m;
            decimal deliveryFee = 30;
            decimal total =subtotal+tax+deliveryFee;
            int paymentId =await _userCartRepo.CreatePaymentAsync(
                    paymentMethod,
                    "Pending",
                    Guid.NewGuid().ToString(),
                    DateTime.Now,
                    total
                );
            int orderId =
                await _userCartRepo.CreateOrderAsync(
                    userId,
                    addressId,
                    paymentId,
                    subtotal,
                    tax,
                    deliveryFee,
                    total
                );
            foreach (var item in cartItems)
            {
                await _userCartRepo.CreateOrderItemAsync(
                    orderId,
                    (int)item.ItemId,
                    (int)item.Quantity,
                    (decimal)item.Item.Price
                );
            }
            await _userCartRepo.DeleteCartAsync(cart.CartId);
            return orderId;
        }
        public async Task<List<CartItem>> GetCartAsync(int userId)
        {
            var cart =await _userCartRepo.GetUserCartAsync(userId);
            if (cart == null)
                return new List<CartItem>();
            return cart.CartItems.ToList();
        }
        public async Task RemoveItemAsync(int cartItemId)
        {
            await _userCartRepo.RemoveCartItemAsync(cartItemId);
        }
        public async Task UpdateItemAsync(int cartItemId, string specialInst, int quantity)
        {
            await _userCartRepo.UpdateCartItemAsync(cartItemId,specialInst,quantity);
        }
    }
}
