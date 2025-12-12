using System;

namespace DevTallesShop.Api.Tests.Services;

public class InMemoryOrderServiceTests
{
  private readonly InMemoryOrderService _sut;
  public InMemoryOrderServiceTests()
  {
    _sut = new InMemoryOrderService();
  }
  [Fact]
  public void GetAll_ReturnsAllOrders()
  {
    var orders = _sut.GetAll();
    orders.Should().NotBeEmpty();
    orders.Should().HaveCount(2);
  }
  [Fact]
  public void GetAll_ReturnsOrdersWithCompleteData()
  {
    var orders = _sut.GetAll().ToList();
    orders.Should().AllSatisfy(o =>
    {
      o.Id.Should().BeGreaterThan(0);
      o.OrderDate.Should().BeBefore(DateTime.UtcNow.AddSeconds(1));
      o.CustomerId.Should().BeGreaterThan(0);
      o.ProductId.Should().BeGreaterThan(0);
      o.Quantity.Should().BeGreaterThan(0);
    });
  }
}
