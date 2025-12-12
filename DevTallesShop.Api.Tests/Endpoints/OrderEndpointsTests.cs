namespace DevTallesShop.Api.Tests.Endpoints;

public class OrderEndpointsTests
{
  [Fact]
  public void GetAllOrders_ReturnsOkWithJoinedData()
  {
    var orders = new[]
     {
      new Order(1, DateTime.UtcNow.AddHours(-2), 30m, 1, 1, 2),
      new Order(2, DateTime.UtcNow.AddHours(-1), 120m, 2, 3, 1)
    };
    var customers = new[]
    {
      new Customer(1,"Devi DevTalles","devi@example.com"),
      new Customer(2,"Teddy Paz","teddy@example.com")
    };
    var products = new[]
    {
      new Product(1, "Mouse", 15m, true),
      new Product(3, "Monitor", 120m, false)
    };

    var orderServiceMock = new Mock<IOrderService>();
    var customerServiceMock = new Mock<ICustomerService>();
    var productServiceMock = new Mock<IProductService>();

    orderServiceMock.Setup(s => s.GetAll()).Returns(orders);
    customerServiceMock.Setup(s => s.GetAll()).Returns(customers);
    productServiceMock.Setup(s => s.GetAllProducts()).Returns(products);

    var result = OrderEndpoints.GetAllOrders(orderServiceMock.Object, customerServiceMock.Object, productServiceMock.Object);
    var okResult = result.Should().BeOfType<Ok<IEnumerable<OrderResponse>>>().Subject;
    okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    okResult.Value.Should().HaveCount(2);
    okResult.Value.Should().AllSatisfy(o =>
    {
      o.OrderDate.Should().BeBefore(DateTime.UtcNow.AddSeconds(1));
      o.CustomerName.Should().NotBeNullOrEmpty();
      o.CustomerEmail.Should().NotBeNullOrEmpty();
      o.ProductName.Should().NotBeNullOrEmpty();
      o.Total.Should().BeGreaterThan(0);
    }
    );
  }
}
