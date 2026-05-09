using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.DAL.Entities.Generated;

namespace Tyaran.BLL.Service.Abstraction
{
    public interface IUserCartService
    {
        Task AddItemAsync(int userId,int itemId,int quantity,string specialInst);
        Task<List<CartItem>> GetCartAsync(int userId);
        Task UpdateItemAsync(int cartItemId,string specialInst,int quantity);
        Task RemoveItemAsync(int cartItemId);
        Task<int> CheckoutAsync(int userId,int addressId,string paymentMethod);
    }
}
