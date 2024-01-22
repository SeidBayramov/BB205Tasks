using App.BUSINESS.DTOs.Brand;
using App.BUSINESS.DTOs.Category;
using App.BUSINESS.DTOs.Course;
using App.BUSINESS.DTOs.Student;
using App.BUSINESS.DTOs.Teacher;
using App.BUSINESS.Services;
using App.BUSINESS.Services.Implementations;
using App.BUSINESS.Services.Interfaces;
using App.CORE.Entities;
using App.DAL.Context;
using App.DAL.Repositories.Implementations;
using App.DAL.Repositories.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddTransient<IValidator<CreateCategoryDto>, CategoryCreateDtoValidator>();
builder.Services.AddTransient<IValidator<UpdateCategoryDto>, CategoryUpdateDtoValidator>();
builder.Services.AddTransient<IValidator<CreateStudentDto>, StudentCreateDtoValidator>();
builder.Services.AddTransient<IValidator<UpdateStudentDto>, StudentUpdateDtoValidator>();
builder.Services.AddTransient<IValidator<CreateTeacherDto>, TeacherCreateDtoValidator>();
builder.Services.AddTransient<IValidator<UpdateTeacherDto>, TeacherUpdateDtoValidator>();
builder.Services.AddTransient<IValidator<CreateCourseDto>, CourseCreateDtoValidator>();
builder.Services.AddTransient<IValidator<UpdateCourseDto>, CourseUpdateDtoValidator>();


builder.Services.AddControllers();
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = false;
    options.User.RequireUniqueEmail = false;
}).AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("myDb1")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
