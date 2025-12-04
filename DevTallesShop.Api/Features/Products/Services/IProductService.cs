using System;
using DevTallesShop.Api.Features.Products.Models;

namespace DevTallesShop.Api.Features.Products.Services;

public interface IProductService
{
  IEnumerable<Product> GetAllProducts();
  Product? GetByIdProduct(int id);
  Product CreateProduct(string name, decimal price, bool inStock);
  bool DeleteProduct(int id);
  bool UpdateProduct(int id, string name, decimal price, bool inStock);

}
