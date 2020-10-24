using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using MakersOfDenmark.Application;
using MakersOfDenmark.Application.Commands.V1;
using MakersOfDenmark.Application.Commands.V1.admin;
using MakersOfDenmark.Infrastructure;
using MakersOfDenmark.WebAPI.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MakersOfDenmark.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              builder =>
                              {
                                  builder.WithOrigins("https://makersofdenmark.azurewebsites.net", 
                                                      "http://makersofdenmark.azurewebsites.net",
                                                      "http://localhost", 
                                                      "https://localhost")
                                                      .AllowAnyHeader()
                                                      .AllowAnyMethod();
                              });
            });
            services.AddControllers(opts => opts.Filters.Add<ModelValidationActionFilter>())
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
            services.AddTransient<IValidatorFactory, ServiceProviderValidatorFactory>();
            services.AddValidatorsFromAssembly(typeof(RegisterMakerSpaceValidator).Assembly);
            services.AddApplicationServiceDependencies();
            services.AddInfrastructureDependencies(Configuration);
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Makers of Denmark API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
