using System;
using DevTallesShop.Api.Data;
using DevTallesShop.Api.Features.Products.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DevTallesShop.Api.Features.Products.Services;

public class InMemoryProductService : IProductService
{
  private readonly List<Product> _products;

  public InMemoryProductService()
  {
    _products = InMemoryDataSeeder.GetProducts();
  }

  public Product CreateProduct(string name, decimal price, bool inStock)
  {
    var nextId = _products.Max(p => p.Id) + 1;
    var product = new Product(nextId, name, price, inStock);
    _products.Add(product);
    return product;
  }

  public bool DeleteProduct(int id)
  {
    var product = GetByIdProduct(id);
    if (product is null)
    {
      return false;
    }
    _products.Remove(product);
    return true;
  }

  public IEnumerable<Product> GetAllProducts() => _products;

  public Product? GetByIdProduct(int id) => _products.FirstOrDefault(p => p.Id == id);

  public bool UpdateProduct(int id, string name, decimal price, bool inStock)
  {
    var product = GetByIdProduct(id);
    if (product is null)
    {
      return false;
    }
    var index = _products.IndexOf(product);
    var updatedProduct = new Product(id, name, price, inStock);
    _products[index] = updatedProduct;
    return true;
  }
}
