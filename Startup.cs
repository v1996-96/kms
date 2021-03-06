﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kms.Data;
using kms.Data.Entities;
using kms.Data.Seed;
using kms.Models;
using kms.Repository;
using kms.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;

namespace kms
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        public static readonly LoggerFactory MyLoggerFactory = new LoggerFactory(new[] {new ConsoleLoggerProvider((_, __) => true, true)});

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            HostingEnvironment = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("KMSDBConnection");
            // services.AddDbContext<KMSDBContext>(options => options.UseLoggerFactory(MyLoggerFactory).UseNpgsql(connectionString));
            services.AddDbContext<KMSDBContext>(options => options.UseNpgsql(connectionString));

            services.AddTransient<IKMSDBConnection, KMSDBConnection>(provider => new KMSDBConnection(connectionString));

            services.AddSingleton<IConfiguration>(Configuration);

            var jwtSection = Configuration.GetSection("jwt");
            var jwtOptions = new JwtOptions();
            jwtSection.Bind(jwtOptions);
            services.Configure<JwtOptions>(jwtSection);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = jwtOptions.ValidateLifetime,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    });

            // Data repositories
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ISearchRepository, SearchRepository>();
            services.AddTransient<IMailingService, MailingService>();

            // Services
            services.AddSingleton<IJwtHandlerService, JwtHandlerService>();
            services.AddSingleton<IMd5HashService, Md5HashService>();
            services.AddSingleton<IAssetsService, AssetsService>();
            services.AddSingleton<IPasswordHasher<Users>, PasswordHasher<Users>>();

            var CorsConfig = Configuration.GetSection("CORS");
            services.AddCors(options => {
                options.AddPolicy("AppCors", builder =>
                    builder.WithOrigins(CorsConfig["host"]).AllowAnyHeader().AllowAnyMethod());
            });

            services.AddMvc().AddJsonOptions(x => {
                x.SerializerSettings.ContractResolver = new DefaultContractResolver {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<KMSDBContext>().EnsureSeeded().Wait();
            }

            app.UseAuthentication();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseCors("AppCors");

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
