using FirsAPIproject.DAL;
using FirsAPIproject.Repositories.Implementations;
using FirsAPIproject.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FirsAPIproject.Entites;
using FirsAPIproject.Services.Implementations;
using FirsAPIproject.Services.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<ICarRepository, CarRepository>();
        builder.Services.AddScoped<IBrandRepository, BrandRepository>();

        builder.Services.AddScoped<IBrandService, BrandService>();
        builder.Services.AddScoped<ICarService, CarService>();


        builder.Services.AddControllers();
        builder.Services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
