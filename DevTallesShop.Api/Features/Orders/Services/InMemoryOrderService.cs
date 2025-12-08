using System;
using DevTallesShop.Api.Data;
using DevTallesShop.Api.Features.Orders.Models;

namespace DevTallesShop.Api.Features.Orders.Services;

public class InMemoryOrderService : IOrderService
{
  private readonly List<Order> _orders;
  public InMemoryOrderService()
  {
    _orders = InMemoryDataSeeder.GetOrders();
  }
  public IEnumerable<Order> GetAll() => _orders;
}
