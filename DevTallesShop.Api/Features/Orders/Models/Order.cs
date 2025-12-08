using System;

namespace DevTallesShop.Api.Features.Orders.Models;

public class Order
{
  public int Id { get; }
  public DateTime OrderDate { get; }
  public decimal Total { get; }
  public int CustomerId { get; }
  public int ProductId { get; }
  public int Quantity { get; }

  public Order(int id, DateTime orderDate, decimal total, int customerId, int productId, int quantity)
  {
    Id = id;
    OrderDate = orderDate;
    CustomerId = customerId;
    ProductId = productId;
    Quantity = quantity;
    Total = total;
  }
}
