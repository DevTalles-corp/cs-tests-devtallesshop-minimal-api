using System;

namespace DevTallesShop.Api.Tests.Integration;

public class CustomerIntegrationTests : IAsyncLifetime
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

  [Fact(DisplayName = "GET /customers debe retornar 200 OK con lista de clientes")]
  public async Task GetAllCustomers_ReturnsOkWithList()
  {
    var response = await _httpClient.GetAsync("/customers");
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    var customers = await response.Content.ReadFromJsonAsync<IEnumerable<CustomerResponse>>();
    customers.Should().NotBeEmpty();
    customers.Should().NotBeNull();
    customers.Should().AllSatisfy(c =>
    {
      c.Id.Should().BeGreaterThan(0);
      c.Name.Should().NotBeNullOrEmpty();
      c.Email.Should().NotBeNullOrEmpty();
    });
  }
  [Fact(DisplayName = "GET /customers debe validar estructura JSON correcta")]
  public async Task GetAllCustomers_ValidatesJsonStructure()
  {
    var response = await _httpClient.GetAsync("/customers");
    var content = await response.Content.ReadAsStringAsync();
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    content.Should().Contain("id");
    content.Should().Contain("name");
    content.Should().Contain("email");
  }
  [Fact(DisplayName = "GET /customers/{id} debe retornar 200 OK cuando existe")]
  public async Task GetCustomerById_ReturnsOkWhenExists()
  {
    var customerId = 1;
    var response = await _httpClient.GetAsync($"/customers/{customerId}");
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    response.Content.Headers.ContentType?.MediaType.Should().Be("application/json");
  }
  [Fact(DisplayName = "GET /customers/{id} valida propiedades del objeto JSON")]
  public async Task GetCustomerById_ValidatesJsonPayload()
  {
    var customerId = 1;
    var response = await _httpClient.GetAsync($"/customers/{customerId}");
    var customer = await response.Content.ReadFromJsonAsync<CustomerResponse>();
    customer?.Id.Should().BeGreaterThan(0);
    customer?.Id.Should().Be(customerId);
    customer?.Name.Should().NotBeNullOrEmpty();
    customer?.Email.Should().NotBeNullOrEmpty();

  }
}