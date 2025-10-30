using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestaurantManagementSystem.Models
{
    public class Receipt
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        [JsonPropertyName("receiptNumber")]
        public string ReceiptNumber { get; set; }

        [JsonPropertyName("paymentMethod")]
        public string PaymentMethod { get; set; }

        [JsonPropertyName("paymentStatus")]
        public string PaymentStatus { get; set; }

        [JsonPropertyName("totalAmount")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("issuedAt")]
        public DateTime IssuedAt { get; set; }

        [JsonPropertyName("customer")]
        public User Customer { get; set; }  // Changed from string to User object

        [JsonPropertyName("order")]
        public Order Order { get; set; }  // Added Order object if needed
    }

    public class ReceiptCreateRequest
    {
        public string OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Discount { get; set; }
    }
}