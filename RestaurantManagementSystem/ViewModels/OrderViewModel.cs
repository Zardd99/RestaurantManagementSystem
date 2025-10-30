using RestaurantManagementSystem.Models;

public class OrderViewModel
{
    public string Id { get; set; }
    public string Customer { get; set; }
    public string Status { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderType { get; set; }
    public int? TableNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public int ItemCount { get; set; }

    public static OrderViewModel FromModel(Order order)
    {
        return new OrderViewModel
        {
            Id = order.Id,
            Customer = order.Customer,
            Status = order.Status,
            TotalAmount = order.TotalAmount,
            OrderType = order.OrderType,
            TableNumber = order.TableNumber,
            OrderDate = order.OrderDate,
            ItemCount = order.Items?.Count ?? 0
        };
    }
}