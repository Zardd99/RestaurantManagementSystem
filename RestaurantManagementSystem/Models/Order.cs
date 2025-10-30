using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestaurantManagementSystem.Models
{
    public class OrderItem
    {
        [JsonPropertyName("menuItem")]
        public string MenuItem { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("specialInstructions")]
        public string SpecialInstructions { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }
    }

    public class Order
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        [JsonPropertyName("items")]
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        [JsonPropertyName("totalAmount")]
        public decimal TotalAmount { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("customer")]
        public string Customer { get; set; }

        [JsonPropertyName("tableNumber")]
        public int? TableNumber { get; set; }

        [JsonPropertyName("orderType")]
        public string OrderType { get; set; }

        [JsonPropertyName("orderDate")]
        public DateTime OrderDate { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
    }

    public class OrderCreateRequest
    {
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal TotalAmount { get; set; }
        public string Customer { get; set; }
        public int? TableNumber { get; set; }
        public string OrderType { get; set; }
    }

    public class OrderStats
    {
        public decimal DailyEarnings { get; set; }
        public decimal WeeklyEarnings { get; set; }
        public decimal YearlyEarnings { get; set; }
        public List<BestSellingDish> BestSellingDishes { get; set; } = new List<BestSellingDish>();
    }

    public class BestSellingDish
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Revenue { get; set; }
    }
}