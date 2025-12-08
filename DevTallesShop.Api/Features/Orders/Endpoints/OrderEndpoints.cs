using System;
using DevTallesShop.Api.Features.Customers.Services;
using DevTallesShop.Api.Features.Orders.Models;
using DevTallesShop.Api.Features.Orders.Services;
using DevTallesShop.Api.Features.Products.Services;

namespace DevTallesShop.Api.Features.Orders.Endpoints;

public static class OrderEndpoints
{
  public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
  {
    app.MapGet("/orders", GetAllOrders);
  }
  public static IResult GetAllOrders(IOrderService orderService, ICustomerService customerService, IProductService productService)
  {
    var orders = orderService.GetAll().ToList();
    var customers = customerService.GetAll().ToList();
    var products = productService.GetAllProducts().ToList();

    var response =
      from o in orders
      join c in customers on o.CustomerId equals c.Id
      join p in products on o.ProductId equals p.Id
      select new OrderResponse(
        o.Id,
        o.OrderDate,
        c.Name,
        c.Email,
        p.Name,
        p.Price,
        o.Quantity,
        o.Total
      );
    return Results.Ok(response);
  }
}
