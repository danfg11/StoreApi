using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using StoreWebApp;
using StoreWebApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


// Configurar HttpClient para conectarse a la API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5209/") });
builder.Services.AddScoped<IProductService, ProductService>();

await builder.Build().RunAsync();
