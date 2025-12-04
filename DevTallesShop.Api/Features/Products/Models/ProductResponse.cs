using System;

namespace DevTallesShop.Api.Features.Products.Models;

public record ProductResponse(int Id, string? Name, decimal Price, bool InStock);
