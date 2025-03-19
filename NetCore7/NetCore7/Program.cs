using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NetCore7.Core;
using NetCore7.Core.Repositories.Contracts;
using NetCore7.Core.Services;
using NetCore7.Infrastructure.Data;
using System;

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
