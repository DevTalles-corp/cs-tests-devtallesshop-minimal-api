using System;
using DevTallesShop.Api.Data;
using DevTallesShop.Api.Features.Customers.Models;

namespace DevTallesShop.Api.Features.Customers.Services;

public class InMemoryCustomerService : ICustomerService
{
  private readonly List<Customer> _customers;
  public InMemoryCustomerService()
  {
    _customers = InMemoryDataSeeder.GetCustomers();
  }
  public IEnumerable<Customer> GetAll() => _customers;

  public Customer? GetById(int id) => _customers.FirstOrDefault(c => c.Id == id);
}
