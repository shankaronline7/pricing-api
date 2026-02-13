using Microsoft.EntityFrameworkCore;
using Pricing.Application;
using Pricing.Infrastructure;
using Pricing.Infrastructure.Persistence;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

#region Services Registration

// 1?? Controllers + JSON Enum as String
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters
            .Add(new JsonStringEnumConverter());
    });

// 2?? Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3?? Application Layer (MediatR, Validators, etc.)
builder.Services.AddApplicationServices();

// 4?? Infrastructure Layer (DbContext, Repositories)
builder.Services.AddInfrastructureServices(builder.Configuration);

#endregion

var app = builder.Build();

#region Middleware Pipeline

// 5?? Swagger Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 6?? HTTPS Redirection
app.UseHttpsRedirection();

// 7?? Authorization
app.UseAuthorization();

// 8?? Map Controllers
app.MapControllers();

#endregion

app.Run();
