using System;
using DevTallesShop.Api.Features.Products.Models;
using DevTallesShop.Api.Features.Products.Services;

namespace DevTallesShop.Api.Features.Products.Endpoints;

public static class ProductsEndpoints
{
  public static MapProductsEndpoints(this IEndpointRouteBuilder app)
  {
    app.MapGet("/products", GetAll);
    app.MapGet("/products/{id:int}", GetProductById);
    app.MapPost("/products", Create);
    app.MapPut("/products/{id:int}", Update);
    app.MapDelete("/products/{id:int}", Delete);
  }
  public static IResult GetAll(IProductService productService)
  {
    var products = productService.GetAllProducts();
    var response = products.Select(p => new ProductResponse(p.Id, p.Name, p.Price, p.InStock));
    return Results.Ok(response);
  }
  public static IResult GetProductById(int id, IProductService productService)
  {
    var product = productService.GetByIdProduct(id);
    if (product is null)
    {
      return Results.NotFound();
    }
    var response = new ProductResponse(product.Id, product.Name, product.Price, product.InStock);
    return Results.Ok(response);
  }
  public static IResult Create(CreateProductRequest request, IProductService productService)
  {
    if (string.IsNullOrWhiteSpace(request.Name))
    {
      return Results.BadRequest("El nombre del producto es requerido.");
    }
    if (request.Price <= 0)
    {
      return Results.BadRequest("El precio debe ser mayor a cero.");
    }
    var product = productService.CreateProduct(request.Name, request.Price, request.InStock);
    var response = new ProductResponse(product.Id, product.Name, product.Price, product.InStock);
    return Results.Created($"/products/{product.Id}", response);
  }
  public record CreateProductRequest(string Name, decimal Price, bool InStock);
}
