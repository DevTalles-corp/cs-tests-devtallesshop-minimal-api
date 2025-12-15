using System;

namespace DevTallesShop.Api.Tests.Integration;

public class ProductIntegrationTests : IAsyncLifetime
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

  [Fact(DisplayName = "POST /products retorna el producto creado con ID asignado")]
  public async Task CreateProduct_ReturnsCreatedProductWithId()
  {
    var newProduct = new { name = "Producto prueba", price = 200m, inStock = true };
    var response = await _httpClient.PostAsJsonAsync("/products", newProduct);
    var createdProduct = await response.Content.ReadFromJsonAsync<ProductResponse>();
    response.StatusCode.Should().Be(HttpStatusCode.Created);
    createdProduct.Should().NotBeNull();
    createdProduct?.Id.Should().BeGreaterThan(0);
    createdProduct?.Name.Should().Be(newProduct.name);
    createdProduct?.Price.Should().Be(newProduct.price);
    createdProduct?.InStock.Should().Be(newProduct.inStock);

  }
  [Fact(DisplayName = "POST /products valida payload JSON correcto")]
  public async Task CreateProduct_ValidatesJsonPayload()
  {
    var newProduct = new { name = "Producto prueba", price = 200m, inStock = true };
    var response = await _httpClient.PostAsJsonAsync("/products", newProduct);
    var content = await response.Content.ReadAsStringAsync();
    content.Should().Contain("id");
    content.Should().Contain("name");
    content.Should().Contain("price");
    content.Should().Contain("inStock");
  }

  [Fact(DisplayName = "PUT /products/{id} debe retornar 200 OK cuando actualiza correctamente", Skip = "Falta implementación")]
  public async Task UpdateProduct_ReturnsOkWhenSuccessful()
  {
  }

  [Fact(DisplayName = "PUT /products/{id} debe retornar 404 cuando el producto no existe", Skip = "Falta implementación")]
  public async Task UpdateProduct_ReturnsNotFoundWhenDoesNotExist()
  {
  }

  [Fact(DisplayName = "DELETE /products/{id} debe retornar 204 No Content", Skip = "Falta implementación")]
  public async Task DeleteProduct_ReturnsNoContent()
  {
  }
  [Fact(DisplayName = "GET /products retorna headers correctos", Skip = "Falta implementación")]
  public async Task GetAllProducts_ReturnsCorrectHeaders()
  {
  }
}