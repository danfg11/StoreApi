using Microsoft.EntityFrameworkCore;
using StoreApi.Data;
using StoreApi.Services;
using StoreApi.Endpoints;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using StoreApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddDbContext<StoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<CartItemService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderDetailService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<RaincheckService>();
builder.Services.AddScoped<StoreService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "StoreApi", Version = "v1" });
});


// Configurar CORS para permitir solicitudes desde todos los orígenes
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configurar opciones de serialización JSON
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.SerializerOptions.WriteIndented = true;
});
var app = builder.Build();

// Asegurarse de que la base de datos esté creada
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<StoreContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

// Configurar el pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoreApi v1");
    });
}


// Aplicar la política de CORS
app.UseCors("AllowAll");

// Mapear endpoints
app.MapCartItemEndpoints();
app.MapCategoryEndpoints();
app.MapOrderEndpoints();
app.MapOrderDetailEndpoints();
app.MapProductEndpoints();
app.MapRaincheckEndpoints();
app.MapStoreEndpoints();

app.Run();
