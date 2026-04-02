using StoresService.Data;
using StoresService.Endpoints;
using StoresService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("StoresService");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Connection string 'ConnectionStrings:StoresService' is required.");
}

builder.Services.AddDbContext<StoresServiceDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

// Register custom services
builder.Services.AddScoped<IStoreService, StoreService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

await InitializeDatabaseAsync(app);

// Map Endpoint slices
app.MapStoreEndpoints();

await app.RunAsync();

static async Task InitializeDatabaseAsync(WebApplication application)
{
    using var scope = application.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<StoresServiceDbContext>();
    if (dbContext.Database.ProviderName?.Contains("Npgsql", StringComparison.OrdinalIgnoreCase) == true)
    {
        await dbContext.Database.MigrateAsync();
        return;
    }

    await dbContext.Database.EnsureCreatedAsync();
}

public partial class Program;
