using AutoMapper;
using Dapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Login.Business;
using Login.Data.Repositories;
using Login.Domain.Models.Request;
using Login.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using static RegisterLogin.API.Filters.ValidateModelFilter;

namespace Login.ServicesCollection
{
    public static class ServicesCollectionsExtensions
    {
        public static IServiceCollection BusinessServices(this IServiceCollection services)
        {
            services.AddTransient<LoginBL>();
            return services;
        }

        public static IServiceCollection RepositoryServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(new Action<IMapperConfigurationExpression>(x => { }), typeof(Startup));
            return services;
        }

        public static IServiceCollection SwaggerServices(this IServiceCollection services)
        {
            services.AddMvcCore().AddApiExplorer();
            services.AddResponseCompression();
            services.AddSwaggerGen(conf =>
            {
                conf.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Login", //VAi ser o título do Swagger
                    Version = "v1"
                });


                // Comentarios Swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                conf.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        public static IServiceCollection DapperServices(this IServiceCollection services)
        {
            services.AddScoped<LoginRepository>();
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            return services;
        }

        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddMvc(options => { options.Filters.Add(typeof(ValidateModelAttribute)); }).AddFluentValidation();

            services.AddScoped<IValidator<LoginRequest>, LoginValidator>();
            services.AddScoped<IValidator<LoginUpdateRequest>, LoginUpdateValidator>();

            return services;
        }
    }
    
}
