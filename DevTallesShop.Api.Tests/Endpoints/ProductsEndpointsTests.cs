using System;
using System.Text.Json;

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
    var notFound = result.Should().BeOfType<NotFound>().Subject;
    notFound.StatusCode.Should().Be(StatusCodes.Status404NotFound);
  }
  [Fact]
  public void CreateProduct_ReturnsCreatedWithPayload()
  {
    var request = new CreateProductRequest("Laptop", 1200m, true);
    var createdProduct = new Product(10, request.Name, request.Price, request.InStock);
    var serviceMock = new Mock<IProductService>();
    serviceMock.Setup(s => s.CreateProduct(request.Name, request.Price, request.InStock)).Returns(createdProduct);

    var result = ProductsEndpoints.Create(request, serviceMock.Object);
    var created = result.Should().BeOfType<Created<ProductResponse>>().Subject;
    // Console.WriteLine("===");
    // Console.WriteLine($"Datos de creación: {JsonSerializer.Serialize(created)}");
    created.StatusCode.Should().Be(StatusCodes.Status201Created);
    created.Value.Should().BeEquivalentTo(new ProductResponse(createdProduct.Id, createdProduct.Name, createdProduct.Price, createdProduct.InStock));
  }
  [Fact]
  public void CreateProduct_ReturnsBadRequest_WhenPriceIsNegative()
  {
    var request = new CreateProductRequest("Laptop", -10, true);
    var serviceMock = new Mock<IProductService>(MockBehavior.Strict);
    var result = ProductsEndpoints.Create(request, serviceMock.Object);
    result.Should().BeOfType<BadRequest<string>>().Which.Value.Should().Be("El precio debe ser mayor a cero.");
  }
  [Fact]
  public void UpdateProduct_ReturnsOk_WhenUpdateSucceeds()
  {
    var productId = 1;
    const string name = "Actualiza producto";
    const decimal price = 25m;
    const bool inStock = false;
    var updatedProduct = new Product(productId, name, price, inStock);
    var serviceMock = new Mock<IProductService>();
    serviceMock.Setup(s => s.UpdateProduct(productId, name, price, inStock)).Returns(true);
    serviceMock.Setup(s => s.GetByIdProduct(productId)).Returns(updatedProduct);

    var request = new UpdateProductRequest(name, price, inStock);
    var result = ProductsEndpoints.Update(productId, request, serviceMock.Object);

    var okResult = result.Should().BeOfType<Ok<ProductResponse>>().Subject;
    okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    okResult.Value.Should().BeEquivalentTo(new ProductResponse(updatedProduct.Id, updatedProduct.Name, updatedProduct.Price, updatedProduct.InStock));
  }
  [Fact]
  public void UpdateProduct_ReturnsNotFound_WhenServiceReturnsFalse()
  {
    var serviceMock = new Mock<IProductService>();
    serviceMock.Setup(s => s.UpdateProduct(It.IsAny<int>(), It.IsAny<string>(),
                                           It.IsAny<decimal>(), It.IsAny<bool>())).Returns(false);
    var request = new UpdateProductRequest("Mouse", 20m, false);
    var result = ProductsEndpoints.Update(99, request, serviceMock.Object);
    var notFound = result.Should().BeOfType<NotFound>().Subject;
    notFound.StatusCode.Should().Be(StatusCodes.Status404NotFound);

  }
  [Fact(Skip = "Pendiente de implementación")]
  public void DeleteProduct_ReturnsNoContent_WhenDeletionSucceeds()
  { }
  [Fact(Skip = "Pendiente de implementación")]
  public void DeleteProduct_ReturnsNotFound_WhenDeletionFails()
  { }
}
