using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Services
{
    public interface IApiClient
    {
        string Token { get; set; }
        Task<LoginResponse> LoginAsync(LoginRequest request);   
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserAsync(string id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(string id, User user);
        Task<bool> DeleteUserAsync(string id);


        Task<List<MenuItem>> GetMenuItemsAsync();
        Task<MenuItem> GetMenuItemAsync(string id);
        Task<MenuItem> CreateMenuItemAsync(MenuItemCreateRequest item);
        Task<MenuItem> UpdateMenuItemAsync(string id, MenuItemCreateRequest item);
        Task<bool> DeleteMenuItemAsync(string id);


        Task<List<Order>> GetOrdersAsync();
        Task<Order> GetOrderAsync(string id);
        Task<Order> CreateOrderAsync(OrderCreateRequest order);
        Task<Order> UpdateOrderAsync(string id, OrderCreateRequest order);
        Task<bool> DeleteOrderAsync(string id);
        Task<Order> UpdateOrderStatusAsync(string id, string status);
        Task<OrderStats> GetOrderStatsAsync();

        Task<List<Review>> GetReviewsAsync();
        Task<Review> CreateReviewAsync(ReviewCreateRequest review);
        Task<Review> UpdateReviewAsync(string id, ReviewCreateRequest review);
        Task<bool> DeleteReviewAsync(string id);

        Task<List<Supplier>> GetSuppliersAsync();
        Task<Supplier> CreateSupplierAsync(SupplierCreateRequest supplier);
        Task<Supplier> UpdateSupplierAsync(string id, SupplierCreateRequest supplier);
        Task<bool> DeleteSupplierAsync(string id);

        Task<List<Receipt>> GetReceiptsAsync();
        Task<Receipt> CreateReceiptAsync(ReceiptCreateRequest receipt);
        Task<Receipt> UpdateReceiptAsync(string id, Receipt receipt);


        Task<List<Category>> GetCategoriesAsync();
        Task<Category> CreateCategoryAsync(CategoryCreateRequest category);
        Task<Category> UpdateCategoryAsync(string id, CategoryCreateRequest category);
        Task<bool> DeleteCategoryAsync(string id);
    }
}