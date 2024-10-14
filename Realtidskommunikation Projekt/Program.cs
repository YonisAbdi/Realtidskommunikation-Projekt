using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Define CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy.WithOrigins("http://localhost:8080")  // Adjust to match your frontend origin
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Allow credentials if needed
    });
});

// Add SignalR to the service collection
builder.Services.AddSignalR();

var app = builder.Build();

// Ensure CORS is applied before any other middleware
app.UseCors("AllowVueApp");

// Enable HTTPS redirection if using HTTPS (adjust if using HTTP only)
//app.UseHttpsRedirection();

app.MapHub<ChattHub>("/chatHub");

await app.RunAsync();
