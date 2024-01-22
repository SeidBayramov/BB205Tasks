using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;
using Udemy.API;
using Udemy.Business.DTOs.AccountDtos;
using Udemy.Business.DTOs.CategoryDtps;
using Udemy.Business.ExternalService.Implementations;
using Udemy.Business.ExternalService.Interface;
using Udemy.Business.Services.Implementations;
using Udemy.Business.Services.Interfaces;
using Udemy.Core.Entities;
using Udemy.DAL.Context;
using Udemy.DAL.Repositories.Implementations;
using Udemy.DAL.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

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
    opt.RegisterValidatorsFromAssembly(typeof(CategoryCreateDtoValidator).Assembly);
    opt.RegisterValidatorsFromAssembly(typeof(AccountRegisterDtoValidator).Assembly);
}).AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
        );

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
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
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

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
