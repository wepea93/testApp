using InventoryService.Api;
using InventoryService.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:8080");
// Ensure the following package is installed:
// Microsoft.EntityFrameworkCore.SqlServer

// Add services to the container.
builder.Services.AddControllers();

// Configuración de API Key
builder.Services.AddAuthentication("ApiKey")
    .AddScheme<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions, ApiKeyAuthHandler>("ApiKey", null);

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Inventory API", Version = "v1", Description = "API para gestión de Inventoryos." });
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath);
});


// Configuración de EF Core con MySQL
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0)), // Cambia por tu versión real,
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()
    ));

builder.Services.AddScoped<InventoryRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthentication();

// Ejecutar migraciones de EF automáticamente al iniciar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();
    db.Database.Migrate();
}

app.UseAuthorization();
app.MapControllers();



app.Run();
