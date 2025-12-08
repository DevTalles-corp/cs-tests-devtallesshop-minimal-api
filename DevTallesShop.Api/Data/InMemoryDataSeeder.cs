using System;
using DevTallesShop.Api.Features.Customers.Models;
using DevTallesShop.Api.Features.Orders.Models;
using DevTallesShop.Api.Features.Products.Models;

namespace DevTallesShop.Api.Data;

public static class InMemoryDataSeeder
{
  public static List<Product> GetProducts() => new()
  {
    new Product(1,"Mouse",15m,true),
    new Product(2,"Teclado",30m,true),
    new Product(3,"Monitor",120m,false),
  };

  public static List<Customer> GetCustomers() => new()
  {
    new Customer(1,"Devi DevTalles","devi@example.com"),
    new Customer(2,"Teddy Paz","teddy@example.com")
  };

  public static List<Order> GetOrders() => new()
  {
    new Order(
      id: 1,
      orderDate: DateTime.UtcNow.AddHours(-2),
      total:30m,
      customerId:1,
      productId:1,
      quantity:2
    ),
    new Order(
      id: 2,
      orderDate: DateTime.UtcNow.AddHours(-1),
      total:120m,
      customerId:2,
      productId:3,
      quantity:1
    )
  };
}
