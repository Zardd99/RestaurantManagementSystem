using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.ViewModels
{
    public class OrderViewModel
    {
        public string Id { get; set; }
        public int ItemCount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string Customer { get; set; }
        public string OrderType { get; set; }
        public DateTime OrderDate { get; set; }
        public string StatusDisplay => Status?.ToUpper();

        public static OrderViewModel FromModel(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                ItemCount = order.Items?.Count ?? 0,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                Customer = order.Customer,
                OrderType = order.OrderType,
                OrderDate = order.OrderDate
            };
        }
    }
}