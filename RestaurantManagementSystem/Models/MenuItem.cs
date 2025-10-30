using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestaurantManagementSystem.Models
{
    public class MenuItem
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("dietaryTags")]
        public List<string> DietaryTags { get; set; } = new List<string>();

        [JsonPropertyName("availability")]
        public bool Availability { get; set; }

        [JsonPropertyName("preparationTime")]
        public int PreparationTime { get; set; }

        [JsonPropertyName("chefSpecial")]
        public bool ChefSpecial { get; set; }

        [JsonPropertyName("averageRating")]
        public double AverageRating { get; set; }

        [JsonPropertyName("reviewCount")]
        public int ReviewCount { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
    }

    public class MenuItemCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        public List<string> DietaryTags { get; set; } = new List<string>();
        public bool Availability { get; set; } = true;
        public int PreparationTime { get; set; } = 15;
        public bool ChefSpecial { get; set; }
    }
}