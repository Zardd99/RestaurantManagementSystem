using System;
using System.Text.Json.Serialization;

namespace RestaurantManagementSystem.Models
{
    public class Category
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
    }

    public class CategoryCreateRequest
    {
        public string Name { get; set; }
    }
}