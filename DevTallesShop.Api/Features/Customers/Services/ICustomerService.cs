using System;
using DevTallesShop.Api.Features.Customers.Models;

namespace DevTallesShop.Api.Features.Customers.Services;

public interface ICustomerService
{
  IEnumerable<Customer> GetAll();
  Customer? GetById(int id);
}
