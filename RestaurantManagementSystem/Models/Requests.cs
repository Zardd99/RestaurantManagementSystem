using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantManagementSystem
{
    // Request models

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
}