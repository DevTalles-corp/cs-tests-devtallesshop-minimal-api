using System;

namespace DevTallesShop.Api.Tests.Services;

public class InMemoryCustomerServiceTests
{
  private readonly InMemoryCustomerService _sut;
  public InMemoryCustomerServiceTests()
  {
    _sut = new InMemoryCustomerService();
  }

  [Fact]
  public void GetAll_ReturnsAllCustomers()
  {
    var customers = _sut.GetAll();
    customers.Should().NotBeEmpty();
    customers.Should().HaveCount(2);
  }

  [Fact]
  public void GetById_ReturnsCustomer_WhenExists()
  {
    var customerId = _sut.GetAll().First().Id;
    var customer = _sut.GetById(customerId);
    customer.Should().NotBeNull();
    customer!.Id.Should().Be(customerId);
    customer.Email.Should().NotBeNullOrEmpty();

  }

  [Fact]
  public void GetById_ReturnsNull_WhenCustomerNotFound()
  {
    var customer = _sut.GetById(999);
    customer.Should().BeNull();
  }
}
