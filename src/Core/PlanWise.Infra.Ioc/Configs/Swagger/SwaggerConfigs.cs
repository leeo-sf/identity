﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace PlanWise.Infra.Ioc.Configs.Swagger;

public static class SwaggerConfigs
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "PlanWise.API",
                    Version = "v1",
                    Description = "API's do sistema PlanWise",
                    License = new OpenApiLicense
                    {
                        Name = "Repositório",
                        Url = new Uri("https://github.com/leeo-sf/identity.git")
                    }
                }
            );
        });
    }

    public static void UseSwaggerConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "PlanWise.API v1");
            });
        }
    }
}
