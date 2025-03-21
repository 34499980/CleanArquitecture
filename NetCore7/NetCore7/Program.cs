using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetCore7.Core;
using NetCore7.Core.Repositories.Contracts;
using NetCore7.Core.Services;
using NetCore7.Infrastructure.Data;
using System;
using System.Text;

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdwda1d8a4sd8w4das8d*w8d*asd@#"))
        };
    });
var app = builder.Build();
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
