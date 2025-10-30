using System.Collections.Generic;
using System.Threading.Tasks;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Services;

namespace RestaurantManagementSystem.Managers
{
    public class UserManager
    {
        private readonly IApiClient _apiClient;

        public UserManager(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _apiClient.GetUsersAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await _apiClient.CreateUserAsync(user);
        }

        public async Task<User> UpdateUserAsync(string id, User user)
        {
            return await _apiClient.UpdateUserAsync(id, user);
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            return await _apiClient.DeleteUserAsync(id);
        }
    }
}