# DevTallesShop.Api
Minimal API de ejemplo para una tienda en línea. Expone recursos de productos, clientes y órdenes, usando datos en memoria para facilitar el aprendizaje y las pruebas locales.

## Características principales

- Arquitectura por features (Products, Customers, Orders) con separación de Endpoints, Models y Services.
- Minimal API en .NET 8 con Swagger/OpenAPI habilitado en modo Development.
- Datos precargados en memoria mediante `InMemoryDataSeeder` (no requiere base de datos).
- Servicios en memoria (`InMemory*Service`) para CRUD básico y consultas.
- Respuestas tipadas con records (por ejemplo, `ProductResponse`, `OrderResponse`).

## Arquitectura del proyecto

Estructura simplificada por carpetas:

```text
DevTallesShop/
  DevTallesShop.Api/
    Data/
      InMemoryDataSeeder.cs        # Carga datos iniciales en memoria
    Features/
      Products/
        Endpoints/                 # Rutas de productos
        Models/                    # Entidades y DTOs
        Services/                  # Lógica en memoria
      Customers/
        Endpoints/
        Models/
        Services/
      Orders/
        Endpoints/
        Models/
        Services/
    Program.cs                     # Configuración de la Minimal API
    DevTallesShop.Api.http         # Archivo de ejemplo para probar rutas
```

En `Program.cs` se registran los servicios en memoria y se mapean los endpoints:

```csharp
builder.Services.AddSingleton<IProductService, InMemoryProductService>();
builder.Services.AddSingleton<ICustomerService, InMemoryCustomerService>();
builder.Services.AddSingleton<IOrderService, InMemoryOrderService>();

app.MapProductsEndpoints();
app.MapCustomerEndpoints();
app.MapOrderEndpoints();
```

## Tecnologías utilizadas

- .NET 8 (`TargetFramework: net8.0`)
- C# 12
- Minimal API
- Swagger / OpenAPI (Swashbuckle)

## Configuración y ejecución

Requisitos previos: SDK de .NET 8 instalado.

Pasos rápidos:

1. Restaurar dependencias: `dotnet restore`
2. Ejecutar la API: `dotnet run --project DevTallesShop.Api`
3. Navegar a Swagger en desarrollo: `http://localhost:5201/swagger`

Puertos por defecto (según `launchSettings.json`):

- HTTP: `http://localhost:5201`
- HTTPS: `https://localhost:7215`

## Endpoints principales

| Recurso   | Método | Ruta              | Descripción                                               |
| --------- | ------ | ----------------- | --------------------------------------------------------- |
| Saludo    | GET    | `/`               | Verifica que la API responde.                             |
| Products  | GET    | `/products`       | Lista todos los productos.                                |
| Products  | GET    | `/products/{id}`  | Obtiene un producto por ID.                               |
| Products  | POST   | `/products`       | Crea un producto (body: `Name`, `Price`, `InStock`).      |
| Products  | PUT    | `/products/{id}`  | Actualiza un producto existente.                          |
| Products  | DELETE | `/products/{id}`  | Elimina un producto.                                      |
| Customers | GET    | `/customers`      | Lista todos los clientes.                                 |
| Customers | GET    | `/customers/{id}` | Obtiene un cliente por ID.                                |
| Orders    | GET    | `/orders`         | Lista órdenes con datos combinados de cliente y producto. |

## Ejemplos de uso

Usa el puerto HTTP (`http://localhost:5201`).

Listar productos:

```bash
curl -X GET "http://localhost:5201/products" -H "accept: application/json"
```

Crear un producto:

```bash
curl -X POST "http://localhost:5201/products" \
  -H "Content-Type: application/json" \
  -d '{"name":"Auriculares","price":45.0,"inStock":true}'
```

Listar órdenes (incluye datos de cliente y producto):

```bash
curl -X GET "http://localhost:5201/orders" -H "accept: application/json"
```

También puedes copiar estas solicitudes al archivo `DevTallesShop.Api.http` y ejecutarlas desde VS Code/Visual Studio.

## Datos de ejemplo

`InMemoryDataSeeder` inicializa listas en memoria para cada recurso:

```csharp
new Product(1, "Mouse", 15m, true);
new Customer(1, "Devi DevTalles", "devi@example.com");
new Order(id: 1, orderDate: DateTime.UtcNow.AddHours(-2), total: 30m, customerId: 1, productId: 1, quantity: 2);
```

Los servicios en memoria (`InMemoryProductService`, `InMemoryCustomerService`, `InMemoryOrderService`) usan estas listas para responder a los endpoints sin necesidad de base de datos externa.

## Pruebas automatizadas

Aún no hay pruebas automatizadas. Se agregarán en futuras iteraciones (unidad e integración sobre los endpoints principales).

## Licencia

No se encontró un archivo de licencia en el repositorio. La licencia aún no está definida.
