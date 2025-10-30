using Microsoft.Extensions.Configuration;
using RestaurantManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestaurantManagementSystem.Services
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public string Token { get; set; }

        public ApiClient(IConfiguration configuration)
        {
            _baseUrl = configuration["ApiSettings:BaseUrl"];
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(int.Parse(configuration["ApiSettings:Timeout"]))
            };
        }

        private void AddAuthorizationHeader()
        {
            if (!string.IsNullOrEmpty(Token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token);
            }
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{_baseUrl}/auth/login", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<LoginResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<List<User>> GetUsersAsync()
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_baseUrl}/users");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<List<User>>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var users = JsonSerializer.Deserialize<List<User>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return users ?? new List<User>();

        }

        public async Task<User> CreateUserAsync(User user)
        {
            AddAuthorizationHeader();
            var content = new StringContent(
                JsonSerializer.Serialize(user),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{_baseUrl}/users", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<User>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.User;
        }

        public async Task<User> UpdateUserAsync(string id, User user)
        {
            AddAuthorizationHeader();
            var content = new StringContent(
                JsonSerializer.Serialize(user),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PutAsync($"{_baseUrl}/users/{id}", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<User>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.User;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/users/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<MenuItem>> GetMenuItemsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/menu");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<MenuItem>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<MenuItem>();
        }

        public async Task<MenuItem> GetMenuItemAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/menu/{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<MenuItem>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<MenuItem> CreateMenuItemAsync(MenuItemCreateRequest item)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(item),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{_baseUrl}/menu", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<MenuItem>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<MenuItem> UpdateMenuItemAsync(string id, MenuItemCreateRequest item)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(item),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PutAsync($"{_baseUrl}/menu/{id}", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<MenuItem>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<bool> DeleteMenuItemAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/menu/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_baseUrl}/orders");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Order>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Order>();
        }

        public async Task<Order> GetOrderAsync(string id)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_baseUrl}/orders/{id}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Order>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<Order> CreateOrderAsync(OrderCreateRequest order)
        {
            AddAuthorizationHeader();
            var content = new StringContent(
                JsonSerializer.Serialize(order),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{_baseUrl}/orders", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Order>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<Order> UpdateOrderAsync(string id, OrderCreateRequest order)
        {
            AddAuthorizationHeader();
            var content = new StringContent(
                JsonSerializer.Serialize(order),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PutAsync($"{_baseUrl}/orders/{id}", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Order>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<bool> DeleteOrderAsync(string id)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/orders/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<Order> UpdateOrderStatusAsync(string id, string status)
        {
            AddAuthorizationHeader();
            var content = new StringContent(
                JsonSerializer.Serialize(new { status }),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PatchAsync($"{_baseUrl}/orders/{id}/status", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Order>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<OrderStats> GetOrderStatsAsync()
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_baseUrl}/orders/stats");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<OrderStats>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        // Review methods
        public async Task<List<Review>> GetReviewsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/reviews");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Review>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Review>();
        }

        public async Task<Review> CreateReviewAsync(ReviewCreateRequest review)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(review),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{_baseUrl}/reviews", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Review>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<Review> UpdateReviewAsync(string id, ReviewCreateRequest review)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(review),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PutAsync($"{_baseUrl}/reviews/{id}", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Review>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<bool> DeleteReviewAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/reviews/{id}");
            return response.IsSuccessStatusCode;
        }

        // Supplier methods
        public async Task<List<Supplier>> GetSuppliersAsync()
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_baseUrl}/suppliers");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Supplier>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Supplier>();
        }

        public async Task<Supplier> CreateSupplierAsync(SupplierCreateRequest supplier)
        {
            AddAuthorizationHeader();
            var content = new StringContent(
                JsonSerializer.Serialize(supplier),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{_baseUrl}/suppliers", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Supplier>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<Supplier> UpdateSupplierAsync(string id, SupplierCreateRequest supplier)
        {
            AddAuthorizationHeader();
            var content = new StringContent(
                JsonSerializer.Serialize(supplier),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PutAsync($"{_baseUrl}/suppliers/{id}", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Supplier>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<bool> DeleteSupplierAsync(string id)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/suppliers/{id}");
            return response.IsSuccessStatusCode;
        }

        // Receipt methods
        public async Task<List<Receipt>> GetReceiptsAsync()
        {
            AddAuthorizationHeader();
            var response = await _httpClient.GetAsync($"{_baseUrl}/receipts");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Receipt>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Receipt>();
        }

        public async Task<Receipt> CreateReceiptAsync(ReceiptCreateRequest receipt)
        {
            AddAuthorizationHeader();
            var content = new StringContent(
                JsonSerializer.Serialize(receipt),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{_baseUrl}/receipts", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Receipt>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<Receipt> UpdateReceiptAsync(string id, Receipt receipt)
        {
            AddAuthorizationHeader();
            var content = new StringContent(
                JsonSerializer.Serialize(receipt),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PutAsync($"{_baseUrl}/receipts/{id}", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Receipt>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        // Category methods
        public async Task<List<Category>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/categories");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Category>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Category>();
        }

        public async Task<Category> CreateCategoryAsync(CategoryCreateRequest category)
        {
            AddAuthorizationHeader();
            var content = new StringContent(
                JsonSerializer.Serialize(category),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{_baseUrl}/categories", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Category>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<Category> UpdateCategoryAsync(string id, CategoryCreateRequest category)
        {
            AddAuthorizationHeader();
            var content = new StringContent(
                JsonSerializer.Serialize(category),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PutAsync($"{_baseUrl}/categories/{id}", content);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Category>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<bool> DeleteCategoryAsync(string id)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/categories/{id}");
            return response.IsSuccessStatusCode;
        }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T User { get; set; }
        public List<T> Users { get; set; }
        public T Data { get; set; }
    }
}