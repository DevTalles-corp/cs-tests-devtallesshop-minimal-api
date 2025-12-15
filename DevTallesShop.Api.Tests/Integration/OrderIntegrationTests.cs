using System;

namespace DevTallesShop.Api.Tests.Integration;

public class OrderIntegrationTests : IAsyncLifetime
{
  private CustomWebApplicationFactory _factory = null!;
  private HttpClient _httpClient = null!;

  public async Task InitializeAsync()
  {
    _factory = new CustomWebApplicationFactory();
    _httpClient = _factory.GetHttpClient();
    await Task.CompletedTask;
  }

  public async Task DisposeAsync()
  {
    _httpClient?.Dispose();
    await _factory.DisposeAsync();
  }
  [Fact(DisplayName = "GET /orders debe retornar 200 OK con lista de órdenes")]
  public async Task GetAllOrders_ReturnsOkWithList()
  {
    var response = await _httpClient.GetAsync("/orders");
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    response.Content.Headers.ContentType?.MediaType.Should().Be("application/json");
    var orders = await response.Content.ReadFromJsonAsync<IEnumerable<OrderResponse>>();
    orders.Should().NotBeNull();
    orders.Should().NotBeEmpty();
  }
  [Fact(DisplayName = "GET /orders retorna órdenes con datos completos")]
  public async Task GetAllOrders_ReturnsOrdersWithCompleteData()
  {
    var response = await _httpClient.GetAsync("/orders");
    var orders = await response.Content.ReadFromJsonAsync<IEnumerable<OrderResponse>>();
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    orders.Should().AllSatisfy(order =>
    {
      order.Id.Should().BeGreaterThan(0);
      order.OrderDate.Should().NotBe(default(DateTime));
      order.CustomerName.Should().NotBeNullOrEmpty();
      order.CustomerEmail.Should().NotBeNullOrEmpty();
      order.ProductName.Should().NotBeNullOrEmpty();
      order.UnitPrice.Should().BeGreaterThan(0);
      order.Quantity.Should().BeGreaterThan(0);
      order.Total.Should().BeGreaterThan(0);
    });
  }
  [Fact(DisplayName = "GET /orders valida cálculo correcto del Total (UnitPrice × Quantity)")]
  public async Task GetAllOrders_CalculatesTotalCorrectly()
  {
    var response = await _httpClient.GetAsync("/orders");
    var orders = await response.Content.ReadFromJsonAsync<IEnumerable<OrderResponse>>();
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    orders.Should().AllSatisfy(o =>
    {
      var expectedTotal = o.UnitPrice * o.Quantity;
      o.Total.Should().Be(expectedTotal, $"Total debe ser UnitPrice({o.UnitPrice}) x Quantity ({o.Quantity})");
    });
  }
  [Fact(DisplayName = "GET /orders retorna las órdenes de forma consistente")]
  public async Task GetAllOrders_ReturnsConsistentResults()
  {
    var response = await _httpClient.GetAsync("/orders");
    var orders = await response.Content.ReadFromJsonAsync<IEnumerable<OrderResponse>>();
    var response2 = await _httpClient.GetAsync("/orders");
    var orders2 = await response2.Content.ReadFromJsonAsync<IEnumerable<OrderResponse>>();
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    response2.StatusCode.Should().Be(HttpStatusCode.OK);
    orders?.Count().Should().Be(orders2?.Count(), "Las llamadas consecutivas deben retonar la misma cantidad de órdenes");
  }
}