using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BookmakerWeb.Services;
using BookmakerWeb.Data;

namespace BookmakerWeb
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Читаем строку подключения из appsettings.json 
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Dependency Injection (DI)
            // Регистрация DbContext в контейнере зависимостей
            builder.Services.AddDbContext<BookmakerDbContext>(options =>
                options.UseSqlite(connectionString));

            // Регистрация кастомного сервиса
            builder.Services.AddScoped<IBetService, BetService>(); // Scoped = один экземпляр на один HTTP-запрос. 

            builder.Services.ConfigureHttpJsonOptions(options =>
                {
                    options.SerializerOptions.WriteIndented = true;  // Это включает форматирование
                    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

            // Создаём экземпляр приложения из настроенного билдера
            var app = builder.Build();

            // Настройка Endpoint'ов
            // Endpoint 1: GET /api/data
            // Внедрение зависимости через параметр метода
            app.MapGet("/api/data", async (IBetService betService) =>
            {
                var bets = await betService.GetAllBetsAsync();
                return Results.Ok(bets);
            })
            .WithName("GetAllData");

            // Endpoint 2: GET /api/config 
            app.MapGet("/api/config", (IConfiguration config) =>
            {
                return Results.Ok(new
                {
                    AppName = config["AppSettings:AppName"],
                    Version = config["AppSettings:Version"],
                    MaxItems = config.GetValue<int>("AppSettings:MaxItems"),
                    Company = config["AppSettings:Company"]
                });
            })
            .WithName("GetConfig");

            // Endpoint 3: GET
            app.MapGet("/", () =>
            {
                return Results.Ok(new
                {
                    Message = "Добро пожаловать в Bookmaker Web API!",
                    Endpoints = new[]
                    {
                        "GET /api/data - все ставки",
                        "GET /api/config - настройки приложения"
                    }
                });
            })
            .WithName("Root");
            
            // Инициализация базы данных
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookmakerDbContext>();
                await dbContext.Database.MigrateAsync();
            }
            // Запуск приложения
            await app.RunAsync();
        }
    }
}