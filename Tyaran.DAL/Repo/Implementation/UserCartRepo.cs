using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyaran.DAL.Database;
using Tyaran.DAL.Entities.Generated;
using Tyaran.DAL.Repo.Abstraction;

namespace Tyaran.DAL.Repo.Implementation
{
    public class UserCartRepo : IUserCartRepo
    {
        private readonly TyaranDbContext _context;
        public UserCartRepo(TyaranDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddToCartAsync(string SpecialInst, int cartId, int itemId, int quantity)
        {
            var cartItemId = _context.Database.SqlQueryRaw<int>(@"EXEC CartItem_Create @SpecialInst, @Quantit@CartID, @ItemID",
            new SqlParameter("@SpecialInst", SpecialInst),
            new SqlParameter("@Quantity", quantity),
            new SqlParameter("@CartID", cartId),
            new SqlParameter("@ItemID", itemId)).FirstOrDefault();
            return cartItemId;
        }
        public async Task<int> CreateCart(int userId)
        {
            var cartId = _context.Database.SqlQueryRaw<int>("EXEC Cart_Create @UserID",
            new SqlParameter("@UserID", userId)).FirstOrDefault();
            return cartId;
        }

        public async Task<int> CreateOrderAsync(int userId, int addressId, int paymentId, decimal subtotal, decimal tax, decimal deliveryFee, decimal totalAmount)
        {
            var orderId = await _context.Database.SqlQueryRaw<int>(@"EXEC Order_Create2
                @UserID,
                @AddressID,
                @PaymentID,
                @Subtotal,
                @Tax,
                @DeliveryFee,
                @TotalAmount",
            new SqlParameter("@UserID", userId),
            new SqlParameter("@AddressID", addressId),
            new SqlParameter("@PaymentID", paymentId),
            new SqlParameter("@Subtotal", subtotal),
            new SqlParameter("@Tax", tax),
            new SqlParameter("@DeliveryFee", deliveryFee),
            new SqlParameter("@TotalAmount", totalAmount)
        )
        .FirstOrDefaultAsync();
            return orderId;
        }

        public async Task CreateOrderItemAsync(int orderId, int itemId, int quantity, decimal unitPrice)
        {
            await _context.Database.ExecuteSqlRawAsync(
                @"EXEC OrderItem_Create
                    @OrderID,
                    @ItemID,
                    @Quantity,
                    @UnitPrice",
                new SqlParameter("@OrderID", orderId),
                new SqlParameter("@ItemID", itemId),
                new SqlParameter("@Quantity", quantity),
                new SqlParameter("@UnitPrice", unitPrice)
            );
        }
        public async Task<int> CreatePaymentAsync(string PaymentMethod, string status, string TransactionId, DateTime PaidAt, decimal amount)
        {
            var paymentId = await _context.Database.SqlQueryRaw<int>(
            @"EXEC Payment_Create
                @PaymentMethod,
                @Status,
                @TransactionId,
                @PaidAt,
                @Amount",

            new SqlParameter("@PaymentMethod", PaymentMethod),
            new SqlParameter("@Status", status),
            new SqlParameter("@TransactionId", TransactionId),
            new SqlParameter("@PaidAt", PaidAt),
            new SqlParameter("@Amount", amount)
        )
        .FirstOrDefaultAsync();
            return paymentId;
        }

        public async Task DeleteCartAsync(int cartId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC Cart_Delete @p0",
                cartId
            );
        }

        public async Task<List<CartItem>> GetCartItemsAsync(int cartId)
        {
            var param = new SqlParameter("@CartID", cartId);
            return await _context.CartItems
                .FromSqlRaw("EXEC CartItems_ReadByCart @CartID", param)
                .Include(ci => ci.Item)
                .Include(ci => ci.Cart)
                .ToListAsync();
        }

        public async Task<Cart?> GetUserCartAsync(int userId)
        {
            return await _context.Carts
        .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Item)
        .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC CartItem_Delete @p0",
                cartItemId
            );
        }
        public async Task UpdateCartItemAsync(int cartItemId, string SpecialInst, int quantity)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_CartItem_Update @p0, @p1, @p2",
                cartItemId,
                SpecialInst,
                quantity
            );
        }
    }
}
