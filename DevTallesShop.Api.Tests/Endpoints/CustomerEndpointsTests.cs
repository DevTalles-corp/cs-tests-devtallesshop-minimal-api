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
  [Fact]
  public void GetCustomerById_ReturnsOk_WhenExists()
  {
    var customer = new Customer(1, "Devi DevTalles", "devi@example.com");
    var serviceMock = new Mock<ICustomerService>();
    serviceMock.Setup(s => s.GetById(1)).Returns(customer);
    var result = CustomerEndpoints.GetCustomer(1, serviceMock.Object);
    var okResult = result.Should().BeOfType<Ok<CustomerResponse>>().Subject;
    okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    okResult.Value.Should().BeEquivalentTo(new CustomerResponse(customer.Id, customer.Name, customer.Email));
  }
  [Fact]
  public void GetCustomerById_ReturnsNotFound_WhenCustomerDoesNotExist()
  {
    var serviceMock = new Mock<ICustomerService>();
    serviceMock.Setup(s => s.GetById(It.IsAny<int>())).Returns((Customer?)null);
    var result = CustomerEndpoints.GetCustomer(999, serviceMock.Object);
    var notFound = result.Should().BeOfType<NotFound>().Subject;
    notFound.StatusCode.Should().Be(StatusCodes.Status404NotFound);
  }
}
