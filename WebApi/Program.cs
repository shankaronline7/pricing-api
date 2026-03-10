using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Models;
using Pricing.Application;
using Pricing.Infrastructure;
using Pricing.Infrastructure.Persistence;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("Issuer Loaded: " + builder.Configuration["Jwt:Issuer"]);
Console.WriteLine("Audience Loaded: " + builder.Configuration["Jwt:Audience"]);
Console.WriteLine("Key Loaded: " + builder.Configuration["Jwt:Key"]);


#region Services Registration

// 1️⃣ Controllers + JSON Enum as String
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters
            .Add(new JsonStringEnumConverter());
    });

// 2️⃣ Swagger
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = "Pricing.WebApi.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    Console.WriteLine("Swagger XML Path: " + xmlPath);

    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath, true);
    }

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});// 3️⃣ Application Layer (MediatR, Validators, etc.)
builder.Services.AddApplicationServices();

// 4️⃣ Infrastructure Layer (DbContext, Repositories)
builder.Services.AddInfrastructureServices(builder.Configuration);

// 5️⃣ JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,

            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]))
        };

        
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("❌ AUTH FAILED: " + context.Exception.ToString());
                return Task.CompletedTask;
            }
        };
    });

/*BUILDER.SERVICES.ADDAUTHORIZATION(OPTIONS =>
{
    OPTIONS.ADDPOLICY("USER_CREATE",
        POLICY => POLICY.REQUIREMENTS.ADD(
            NEW PERMISSIONREQUIREMENT("USER_CREATE")));

    OPTIONS.ADDPOLICY("USER_UPDATE",
        POLICY => POLICY.REQUIREMENTS.ADD(
            NEW PERMISSIONREQUIREMENT("USER_UPDATE")));

    OPTIONS.ADDPOLICY("USER_DELETE",
        POLICY => POLICY.REQUIREMENTS.ADD(
            NEW PERMISSIONREQUIREMENT("USER_DELETE")));

    OPTIONS.ADDPOLICY("USER_VIEW",
        POLICY => POLICY.REQUIREMENTS.ADD(
            NEW PERMISSIONREQUIREMENT("USER_VIEW")));
});*/


#endregion

var app = builder.Build();

#region Middleware Pipeline

// 6️⃣ Swagger Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 7️⃣ HTTPS Redirection
app.UseHttpsRedirection();

// 🔐 IMPORTANT: Authentication must come BEFORE Authorization
app.UseAuthentication();
app.UseAuthorization();

// 8️⃣ Map Controllers
app.MapControllers();

#endregion

app.Run();
