using System;

namespace DevTallesShop.Api.Tests.Services;

public class InMemoryProductServiceTests
{
  private readonly InMemoryProductService _sut;
  public InMemoryProductServiceTests()
  {
    _sut = new InMemoryProductService();
  }
  [Fact]
  public void Create_ShouldAddProductWithIncrementalId()
  {
    var created = _sut.CreateProduct("Celular", 55m, true);
    created.Id.Should().BeGreaterThan(0);
    _sut.GetAllProducts().Should().ContainEquivalentOf(created);
  }

  [Fact]
  public void Update_ShouldPersistChanges_WhenProductExists()
  {
    var existingId = _sut.GetAllProducts().First().Id;
    var updated = _sut.UpdateProduct(existingId, "Producto actulizado", 25m, false);
    updated.Should().BeTrue();
    var product = _sut.GetByIdProduct(existingId);
    product!.Name.Should().Be("Producto actulizado");
    product.Price.Should().Be(25m);
    product.InStock.Should().BeFalse();
  }

  [Fact]
  public void Update_ShouldReturnFalse_WhenProductNotFound()
  {
    var updated = _sut.UpdateProduct(999, "No encontrado", 10m, false);
    updated.Should().BeFalse();
  }

  [Fact]
  public void Delete_ShouldReturnFalse_WhenProductNotFound()
  {
    var updated = _sut.DeleteProduct(999);
    updated.Should().BeFalse();
  }
}
