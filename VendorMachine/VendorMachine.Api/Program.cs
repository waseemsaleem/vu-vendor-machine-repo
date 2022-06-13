#nullable disable
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VendorMachine.Core.Data;
using VendorMachine.Core.DTOs;
using VendorMachine.Core.Middlewares;
using VendorMachine.Core.Services;
using VendorMachine.Core.Services.Interfaces;
using VendorMachine.Core.ViewModels;

namespace VendorMachine.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Register Application Db Context.
            builder.Services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ApiDbConnectionString"));
            });
            #endregion

            #region Register Interfaces

            builder.Services.AddScoped<IJWTService, JWTService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddSingleton<IUserAdapter, UserAdapter>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddSingleton<IProductAdapter, ProductAdapter>();
            builder.Services.AddScoped<IRoleService, RoleServiceService>();
            builder.Services.AddSingleton<IRoleAdapter, RoleAdapter>();
            builder.Services.AddScoped<IUserRole, UserRoleService>();
            builder.Services.AddSingleton<IUserRoleAdapter, UserRoleAdapter>();
            #endregion

            #region JWT configuration
            var bindJwtSettings = new JwtSettings(builder.Configuration);
            builder.Configuration.Bind("JsonWebTokenKeys", bindJwtSettings);
            builder.Services.AddSingleton(bindJwtSettings);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateActor = bindJwtSettings.ValidateIssuer,
                    ValidateAudience = bindJwtSettings.ValidateAudience,
                    ValidateLifetime = bindJwtSettings.RequireExpirationTime,
                    ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
                    ValidIssuer = bindJwtSettings.ValidIssuer,
                    ValidAudience = bindJwtSettings.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
                    RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                    ClockSkew = TimeSpan.FromDays(1)
                };
            });
            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            #region Swagger Configuration
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<JwtMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}