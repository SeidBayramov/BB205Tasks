using APIproject.DAL;
using APIproject.Repositories.Implementations;
using APIproject.Repositories.Interface;
using APIproject.Services.Interface;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace APIproject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

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
}