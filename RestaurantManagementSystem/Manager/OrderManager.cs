using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Managers
{
    public class OrderManager
    {
        private readonly IApiClient _apiClient;

        public OrderManager(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _apiClient.GetOrdersAsync();
        }

        public async Task<Order> CreateOrderAsync(OrderCreateRequest order)
        {
            return await _apiClient.CreateOrderAsync(order);
        }

        public async Task<Order> UpdateOrderStatusAsync(string id, string status)
        {
            return await _apiClient.UpdateOrderStatusAsync(id, status);
        }

        public async Task<OrderStats> GetOrderStatsAsync()
        {
            return await _apiClient.GetOrderStatsAsync();
        }
    }
}