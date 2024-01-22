using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System;
using Udemy.API;
using Udemy.Business.DTOs.CategoryDtps;
using Udemy.Business.Services.Implementations;
using Udemy.Business.Services.Interfaces;
using Udemy.DAL.Context;
using Udemy.DAL.Repositories.Implementations;
using Udemy.DAL.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
builder.Services.AddControllers().AddFluentValidation(opt =>
{
    opt.RegisterValidatorsFromAssembly(typeof(CreateCategoryDtoValidator).Assembly);
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
