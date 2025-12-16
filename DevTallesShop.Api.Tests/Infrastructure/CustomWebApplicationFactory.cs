public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    base.ConfigureWebHost(builder);
    builder.ConfigureServices(services =>
    {
      // Aquí podríamos remover o reemplazar servicios si fuera necesario
      // Por ejemplo, si tuviéramos una base de datos real, podríamos usar SQLite en memoria para tests
    });
    builder.UseEnvironment("Testing");
  }
  public HttpClient GetHttpClient()
  {
    var client = CreateClient();
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    return client;
  }
}
