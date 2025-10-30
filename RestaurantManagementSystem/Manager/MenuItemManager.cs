using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Services;

namespace RestaurantManagementSystem.Managers
{
    public class MenuItemManager
    {
        private readonly IApiClient _apiClient;

        public MenuItemManager(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<List<MenuItem>> GetMenuItemsAsync()
        {
            return await _apiClient.GetMenuItemsAsync();
        }

        public async Task<MenuItem> GetMenuItemAsync(string id)
        {
            return await _apiClient.GetMenuItemAsync(id);
        }

        public async Task<MenuItem> CreateMenuItemAsync(MenuItemCreateRequest menuItem)
        {
            return await _apiClient.CreateMenuItemAsync(menuItem);
        }

        public async Task<MenuItem> UpdateMenuItemAsync(string id, MenuItemCreateRequest menuItem)
        {
            return await _apiClient.UpdateMenuItemAsync(id, menuItem);
        }

        public async Task<bool> DeleteMenuItemAsync(string id)
        {
            return await _apiClient.DeleteMenuItemAsync(id);
        }

        public async Task<List<MenuItem>> GetAvailableMenuItemsAsync()
        {
            var allItems = await GetMenuItemsAsync();
            return allItems.FindAll(item => item.Availability);
        }

        public async Task<List<MenuItem>> GetMenuItemsByCategoryAsync(string categoryId)
        {
            var allItems = await GetMenuItemsAsync();
            return allItems.FindAll(item => item.Category?.Id == categoryId);
        }

        public async Task<List<MenuItem>> GetChefSpecialsAsync()
        {
            var allItems = await GetMenuItemsAsync();
            return allItems.FindAll(item => item.ChefSpecial);
        }
    }
}