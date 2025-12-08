namespace DevTallesShop.Api.Features.Orders.Models;

public record OrderResponse(
  int Id,
  DateTime OrderDate,
  string? CustomerName,
  string? CustomerEmail,
  string? ProductName,
  decimal UnitPrice,
  int Quantity,
  decimal Total
);
