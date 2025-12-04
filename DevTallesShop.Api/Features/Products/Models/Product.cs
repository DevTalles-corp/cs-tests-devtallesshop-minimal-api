using System;

namespace DevTallesShop.Api.Features.Products.Models;

public class Product
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public decimal Price { get; set; }
  public bool InStock { get; set; }

  public Product(int id, string name, decimal price, bool inStock)
  {
    Id = id;
    Name = name;
    Price = price;
    InStock = inStock;
  }
}
