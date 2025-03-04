using Microsoft.EntityFrameworkCore;
using Mini_ERP_System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Load API Key from Configuration
var apiKey = builder.Configuration["ApiKey"];
if (string.IsNullOrEmpty(apiKey))
{
    throw new Exception("API Key is missing in appsettings.json.");
}


// Enable Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Custom API Key Middleware (ONLY for Admin Routes)
app.Use(async (context, next) =>
{
    var path = context.Request.Path.ToString().ToLower();

    // Check if the request is for admin endpoints (Modify as needed)
    if (path.StartsWith("/api/admin"))
    {
        if (context.Request.Headers.TryGetValue("apiKey", out var extractedApiKey) && extractedApiKey == apiKey)
        {
            await next(); // API Key is valid, proceed
            return;
        }

        context.Response.StatusCode = 401; // Unauthorized
        await context.Response.WriteAsync("Unauthorized: Invalid API Key.");
        return;
    }

    await next(); // Proceed for non-admin routes
});

// for frontend connection
app.UseCors(builder =>
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();
