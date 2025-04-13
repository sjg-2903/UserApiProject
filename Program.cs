using UserApiProject.Middleware;
using UserApiProject.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddControllers();
builder.Services.AddSingleton<UserService>();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "User API",
        Version = "v1"
    });
});

var app = builder.Build();

// Swagger Middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "User API V1");
});

app.UseHttpsRedirection();

// Custom logging middleware
app.UseRequestLogger();

app.UseAuthorization();

app.MapControllers();

app.Run();
