using System;

namespace DevTallesShop.Api.Tests.Endpoints;

public class CustomerEndpointsTests
{
  [Fact]
  public void GetAllCustomers_ReturnsOkWithCustomers()
  {
    var serviceMock = new Mock<ICustomerService>();
    var customers = new[]
    {
      new Customer(1,"Devi DevTalles","devi@example.com"),
      new Customer(2,"Teddy Paz","teddy@example.com")
    };
    serviceMock.Setup(s => s.GetAll()).Returns(customers);
    var result = CustomerEndpoints.GetAllCustomers(serviceMock.Object);
    var okResult = result.Should().BeOfType<Ok<IEnumerable<CustomerResponse>>>().Subject;
    okResult.Value.Should().HaveCount(2);
    okResult.Value.Should().AllSatisfy(c =>
    {
      c.Id.Should().BeGreaterThan(0);
      c.Name.Should().NotBeNullOrEmpty();
      c.Email.Should().NotBeNullOrEmpty();
    }
    );
  }
  [Fact(Skip = "Falta implemetación")]
  public void GetCustomerById_ReturnsOk_WhenExists()
  { }
  [Fact(Skip = "Falta implemetación")]
  public void GetCustomerById_ReturnsNotFound_WhenCustomerDoesNotExist()
  { }
}
