using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.DAL.Entities.Generated;

namespace Tyaran.DAL.Repo.Abstraction
{
    public interface IUserCartRepo
    {
        Task<int> AddToCartAsync(string SpecialInst, int cartId, int itemId, int quantity);
        Task UpdateCartItemAsync(int cartId, string SpecialInst, int quantity);
        Task RemoveCartItemAsync(int cartId);
        Task<List<CartItem>> GetCartItemsAsync(int cartId);
        Task<int> CreateCart(int userId);
        Task<Cart?> GetUserCartAsync(int userId);
        Task DeleteCartAsync(int cartId);
        Task<int> CreatePaymentAsync(string PaymentMethod, string status, string TransactionId, DateTime PaidAt, decimal amount);
        Task<int> CreateOrderAsync(int userId,int addressId,int paymentId,decimal subtotal,decimal tax,decimal deliveryFee,decimal totalAmount);
        Task CreateOrderItemAsync(int orderId,int itemId,int quantity,decimal unitPrice);
    }
}
