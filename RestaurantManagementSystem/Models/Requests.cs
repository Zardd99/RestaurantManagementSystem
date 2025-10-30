using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagementSystem
{
    // Request models
    public class ReviewCreateRequest
    {
        [Required]
        public string MenuItemId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        public string Comment { get; set; }

        [Required]
        public string CustomerName { get; set; }
    }

    public class SupplierCreateRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ContactPerson { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        public string Address { get; set; }
    }

    public class ReceiptCreateRequest
    {
        [Required]
        public string OrderId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public string Notes { get; set; }
    }

    public class CategoryCreateRequest
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }

    // If you need MenuItemForm (alternative to MenuItemCreateRequest)
    public partial class MenuItemForm
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string CategoryId { get; set; }

        public string ImageUrl { get; set; }

        public bool IsAvailable { get; set; } = true;

        public string Ingredients { get; set; }

        [Range(0, int.MaxValue)]
        public int PreparationTime { get; set; }
    }
}