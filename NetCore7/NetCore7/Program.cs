using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetCore7.API.Middleware;
using NetCore7.Core;
using NetCore7.Core.Repositories.Contracts;
using NetCore7.Core.Services;
using NetCore7.Core.Services.Contracts.Security;
using NetCore7.Infrastructure.Data;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

string APIName = "CT API";
string APIVersion = "v0.0.1";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
builder.Services.AddScoped(typeof(IContextProvider), typeof(ContextProvider));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

//builder.Services.AddTransient<TokenRefreshMiddleware>();


builder.Services.AddDbContext<DefaultContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default"),
        x => x.MigrationsAssembly("NetCore7.Infrastructure"))
            );

    builder.Services.AddCors(options => {
        options.AddDefaultPolicy(x => { x.WithOrigins("*"); });
    });
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "http://localhost:7169",  // El emisor esperado del token
            ValidAudience = "http://localhost:4200",  // La audiencia esperada
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("N2QwNWI2OGYzZWJjMzYxZTU5MmNlMTM3YjEzYmU0ZGRmZDFkMjAxZDZiNjQ="))
        };
      /*  options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                // Lógica para manejar errores de autenticación si el token ha expirado
                context.HandleResponse();  // Evita la respuesta por defecto
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsync("{\"message\": \"Token expirado o inválido\"}");
            }
        };,*/
    });
    builder.Services.AddSwaggerGen(options =>
{

    options.OperationFilter<SelectedTenantIdHeader>();
    options.SwaggerDoc("v1", new OpenApiInfo { Title = APIName, Version = APIVersion });
    const string schemeName = "Bearer";
    options.AddSecurityDefinition(schemeName, new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = schemeName

    });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = schemeName
                            },
                            Scheme = "oauth2",
                            Name = schemeName,
                            In = ParameterLocation.Header,
                        },new List<string>()
                    }});
    });


var app = builder.Build();
app.Use(async (context, next) =>
{
    var authorizationHeader = context.Request.Headers["Authorization"].ToString();

    if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
    {
        var token = authorizationHeader.Substring("Bearer ".Length).Trim();

        if (!EsTokenValido(token))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("El token ha expirado o es inválido.");
            return; // Detiene la ejecución del pipeline si el token es inválido
        }
    }

    await next(); // Llama al siguiente middleware (como la autenticación)
});
//app.UseMiddleware<TokenRefreshMiddleware>(); // Agregarlo a la cadena de middleware

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

 bool EsTokenValido(string token)
{
    var handler = new JwtSecurityTokenHandler();
    var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

    if (jsonToken != null)
    {
        var exp = jsonToken.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;
        if (exp != null)
        {
            var expDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp)).DateTime;
            if (expDate < DateTime.UtcNow)
            {
                return false; // El token ha expirado
            }
        }
    }

    return true; // El token sigue siendo válido
}
public class SelectedTenantIdHeader : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "SelectedTenantId",
            In = ParameterLocation.Header,
            // set to false if this is optional,
        });
    }
   
}

