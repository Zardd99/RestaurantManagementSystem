using System;
using System.Text.Json.Serialization;

namespace RestaurantManagementSystem.Models
{
    public class Review
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        [JsonPropertyName("user")]
        public User User { get; set; }  // Changed from string to User object

        [JsonPropertyName("menuItem")]
        public MenuItem MenuItem { get; set; }  // Changed from string to MenuItem object

        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }

    public class ReviewCreateRequest
    {
        public string User { get; set; }
        public string MenuItem { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}