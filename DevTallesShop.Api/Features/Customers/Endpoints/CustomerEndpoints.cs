using System;
using DevTallesShop.Api.Features.Customers.Models;
using DevTallesShop.Api.Features.Customers.Services;

namespace DevTallesShop.Api.Features.Customers.Endpoints;

public static class CustomerEndpoints
{
  public static void MapCustomerEndpoints(this IEndpointRouteBuilder app)
  {
    app.MapGet("/customers", GetAllCustomers);
    app.MapGet("/customers/{id:int}", GetCustomer);
  }
  public static IResult GetAllCustomers(ICustomerService customerService)
  {
    var customers = customerService.GetAll();
    var response = customers.Select(c => new CustomerResponse(c.Id, c.Name, c.Email));
    return Results.Ok(response);
  }

  public static IResult GetCustomer(int id, ICustomerService customerService)
  {
    var customer = customerService.GetById(id);
    if (customer is null)
    {
      return Results.NotFound();
    }
    var response = new CustomerResponse(customer.Id, customer.Name, customer.Email);
    return Results.Ok(response);
  }


}
