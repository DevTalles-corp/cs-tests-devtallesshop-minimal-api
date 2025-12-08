using System;
using DevTallesShop.Api.Features.Orders.Models;

namespace DevTallesShop.Api.Features.Orders.Services;

public interface IOrderService
{
  IEnumerable<Order> GetAll();
}
