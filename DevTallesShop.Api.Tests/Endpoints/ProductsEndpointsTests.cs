using System;

namespace DevTallesShop.Api.Tests.Endpoints;

public class ProductsEndpointsTests
{
  [Fact]
  public void GetAllProducts_ReturnsOkWithProducts()
  {
    var products = new[]
    {
      new Product(1,"Mouse",15m,true),
      new Product(2,"Teclado",30m,true),
      new Product(3,"Monitor",120m,false)
    };
    var serviceMock = new Mock<IProductService>();
    serviceMock.Setup(s => s.GetAllProducts()).Returns(products);

    var result = ProductsEndpoints.GetAll(serviceMock.Object);
    var okResult = result.Should().BeOfType<Ok<IEnumerable<ProductResponse>>>().Subject;
    // Console.WriteLine("=======");
    // Console.WriteLine(okResult);
    okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    okResult.Value.Should().HaveCount(3);
    okResult.Value.Should().AllSatisfy(p =>
    {
      p.Id.Should().BeGreaterThan(0);
      p.Name.Should().NotBeNullOrEmpty();
      p.Price.Should().BeGreaterThan(0);
    });
  }
  [Fact]
  public void GetProductById_ReturnsOKWhithProduct()
  {
    var product = new Product(1, "Laptop", 120m, true);
    var serviceMock = new Mock<IProductService>();
    serviceMock.Setup(s => s.GetByIdProduct(1)).Returns(product);

    var result = ProductsEndpoints.GetProductById(1, serviceMock.Object);
    var foundProduct = result.Should().BeOfType<Ok<ProductResponse>>().Subject;
    foundProduct.StatusCode.Should().Be(StatusCodes.Status200OK);
  }

  [Fact]
  public void GetProductById_ReturnsNotFound_WhenProductDoesNotExist()
  {
    var serviceMock = new Mock<IProductService>();
    serviceMock.Setup(s => s.GetByIdProduct(It.IsAny<int>())).Returns((Product?)null);

    var result = ProductsEndpoints.GetProductById(45, serviceMock.Object);
    var foundProduct = result.Should().BeOfType<NotFound>().Subject;
    foundProduct.StatusCode.Should().Be(StatusCodes.Status404NotFound);
  }
  [Fact(Skip = "Pendiente de implementación")]
  public void CreateProduct_ReturnsCreatedWithPayload()
  { }
  [Fact(Skip = "Pendiente de implementación")]
  public void CreateProduct_ReturnsBadRequest_WhenPriceIsNegative()
  { }
  [Fact(Skip = "Pendiente de implementación")]
  public void UpdateProduct_ReturnsOk_WhenUpdateSucceeds()
  { }
  [Fact(Skip = "Pendiente de implementación")]
  public void UpdateProduct_ReturnsNotFound_WhenServiceReturnsFalse()
  { }
  [Fact(Skip = "Pendiente de implementación")]
  public void DeleteProduct_ReturnsNoContent_WhenDeletionSucceeds()
  { }
  [Fact(Skip = "Pendiente de implementación")]
  public void DeleteProduct_ReturnsNotFound_WhenDeletionFails()
  { }
}
