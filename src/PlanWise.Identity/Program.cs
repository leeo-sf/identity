using Microsoft.AspNetCore.Identity;
using PlanWise.Domain.Entities;
using PlanWise.Infra.Data.Context;
using PlanWise.Infra.Ioc.DependencyInjection;
using PlanWise.Presentation;
using RabbitMQServer.services;

var builder = WebApplication.CreateBuilder(args);
var structureDependencies = new DependencyInjectionIdentity(
    builder.Services,
    builder.Configuration
);

structureDependencies.AddDbContext();

structureDependencies.AddScopedAndDependencies();
builder.Services.AddHostedService<RabbitMQConfirmEmailConsumer>();
builder.Services.AddHostedService<RabbitMQ2FAConsumer>();
builder.Services.AddHostedService<RabbitMQForgetPasswordConsumer>();

builder
    .Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContextIdentity>()
    .AddDefaultTokenProviders();

structureDependencies.ConfigurePasswordRules();

builder.Services.AddAuthorization();

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.AddEndPoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
