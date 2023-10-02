using System.Text;
using API.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// .AddApplicationServices() and AddIdentityServices() are extension methods in API.Extensions
builder.Services.AddApplicationServices(builder.Configuration);
// Add Authentication Configuration (installed jwtbearer nuget package)
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

// Authentication and Authorization MUST be AFTER UserCors and BEFORE MapControllers()
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();