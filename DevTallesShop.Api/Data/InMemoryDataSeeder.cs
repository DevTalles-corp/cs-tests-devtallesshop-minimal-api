using System;
using DevTallesShop.Api.Features.Customers.Models;
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
}
