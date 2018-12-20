using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhotoSearch.Api.ActionDecorators;
using PhotoSearch.Api.Authentication;
using PhotoSearch.Api.Exceptions.Adapters;
using PhotoSearch.Api.Infrastructure;
using PhotoSearch.Api.Pipelines;
using PhotoSearch.Api.Services;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace PhotoSearch.Api
{
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services) {
            services.AddMemoryCache();


            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info { Title = "Photo Search API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
                c.DescribeAllEnumsAsStrings();

                c.OperationFilter<AuthorizationFilterAttribute>();
            });

            services.AddSingleton<IAbiokaToken, AbiokaToken>();
            services.AddSingleton<ICache, MemoryCache>();
            services.AddSingleton<IHttpClient, CustomHttpClient>();
            services.AddSingleton<IExceptionAdapterFactory, ExceptionAdapterFactory>();
            services.AddTransient<IUrlBuilder, FlickrUrlBuilder>();
            services.AddTransient<IPhotoService, FlickrPhotoService>();

            services.AddMediatR(typeof(Startup));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(CachePipelineBehavior<,>));
            
            services.AddScoped<IActionBeforeDecorator, BearerAuthenticationDecorator>();
            services.AddScoped<IActionExceptionDecorator, ExceptionDecorator>();
            services.AddScoped<ActionDecoratorFilter>();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(s => {
                s.SwaggerEndpoint("../swagger/v1/swagger.json", "PhotoSearch");
            });
        }
    }
}
