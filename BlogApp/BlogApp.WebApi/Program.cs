using BlogApp.Business;
using BlogApp.Business.Services.Implementations;
using BlogApp.Business.Services.Interface;
using BlogApp.Core.Entities;
using BlogApp.DAL.Context;
using BlogApp.DAL.Repositories.Implemenetations;
using BlogApp.DAL.Repositories.Interface;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using BlogApp.Business.ExternalService.Interface;
using BlogApp.Business.ExternalService.Implementations;

namespace BlogApp.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddAutoMapper(typeof(MapperProfile));

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigninKey"])),
                    LifetimeValidator = (_, expireddate, token, _) => token != null ? expireddate > DateTime.UtcNow : false,
                };
            }
            );
            builder.Services.AddControllers().AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssembly(typeof(MapperProfile).Assembly);
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
             });
            });


            builder.Services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
