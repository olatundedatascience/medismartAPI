using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MedismartsAPI.Model;
using MedismartsAPI.Services;
using MedismartsAPI.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MedismartsAPI
{
    public class Startup
    {
        public IConfiguration config;
        private JwtSettings jwtSettings;
        private DbSettings DbSettings;
        public Startup(IConfiguration config)
        {
            this.config = config;
            jwtSettings = new JwtSettings();
            DbSettings = new DbSettings();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            config.Bind(nameof(JwtSettings), jwtSettings);
            config.Bind(nameof(DbSettings), DbSettings);
            var tokenValidationParamaters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.JwtKey)),
                ValidateIssuer = false,
                ValidateAudience = false

            };
            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(x =>
            //{
            //    x.SaveToken = true;
            //    x.TokenValidationParameters = tokenValidationParamaters;


            //});

            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<StudentDbContext>(opts =>
            {
                opts.UseSqlServer(DbSettings.url);
            });

            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MEDISMART API",
                    Description = "MEDISMART API"

                });


            });
            services.AddCors(options=>
            {
                // for client to be able send request
                options.AddPolicy("AllowOrigin",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
                  //  .AllowCredentials());
            });


            services.AddTransient<HandleException>();
            services.AddTransient<IUtilityService, UtilityService>();
            services.AddTransient<StudentsService, StudentsInformationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowOrigin");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

         //   app.UseAuthentication();
          //  app.UseAuthorization();

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "/swagger/{documentName}/swagger.json";
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web hooks API V1");
                //c.RoutePrefix = "InternetBankingRIB/swagger";

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
