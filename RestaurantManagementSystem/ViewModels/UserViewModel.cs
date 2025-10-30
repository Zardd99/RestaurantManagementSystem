using System;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public static UserViewModel FromModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Phone = user.Phone,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }
    }
}