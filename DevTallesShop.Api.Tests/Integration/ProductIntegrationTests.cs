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

  [Fact(DisplayName = "PUT /products/{id} debe retornar 200 OK cuando actualiza correctamente")]
  public async Task UpdateProduct_ReturnsOkWhenSuccessful()
  {
    var productId = 1;
    var updatedProduct = new { name = "Producto actualizado", price = 300m, inStock = false };
    var response = await _httpClient.PutAsJsonAsync($"/products/{productId}", updatedProduct);
    var updated = await response.Content.ReadFromJsonAsync<ProductResponse>();
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    updated.Should().NotBeNull();
    updated?.Id.Should().BeGreaterThan(0);
    updated?.Name.Should().Be(updatedProduct.name);
    updated?.Price.Should().Be(updatedProduct.price);
    updated?.InStock.Should().Be(updatedProduct.inStock);
  }

  [Fact(DisplayName = "PUT /products/{id} debe retornar 404 cuando el producto no existe")]
  public async Task UpdateProduct_ReturnsNotFoundWhenDoesNotExist()
  {
    var productId = 9999;
    var updatedProduct = new { name = "Producto no encontrado", price = 300m, inStock = false };
    var response = await _httpClient.PutAsJsonAsync($"/products/{productId}", updatedProduct);
    response.StatusCode.Should().Be(HttpStatusCode.NotFound);
  }

  [Fact(DisplayName = "DELETE /products/{id} debe retornar 204 No Content")]
  public async Task DeleteProduct_ReturnsNoContent()
  {
    var productId = 1;
    var response = await _httpClient.DeleteAsync($"/products/{productId}");
    response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    response.Content.Headers.ContentLength.Should().Be(0);
  }
  [Fact(DisplayName = "GET /products retorna headers correctos")]
  public async Task GetAllProducts_ReturnsCorrectHeaders()
  {
    var response = await _httpClient.GetAsync("/products");
    response.StatusCode.Should().Be(HttpStatusCode.OK);
    response.Content.Headers.ContentType?.MediaType.Should().Be("application/json");
    response.Content.Headers.ContentLength.Should().BeGreaterThan(0);
  }
}