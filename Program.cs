using DCapProject.Interfaces;
using DCapProject.Models;
using DCapProject.Services;

namespace DCapProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Configure Cosmos settings
            var cosmosSettings = new CosmosSettings();
            builder.Configuration.GetSection("Cosmos").Bind(cosmosSettings);

            builder.Services.AddSingleton(cosmosSettings);

            builder.Services.AddControllers();
            builder.Services.AddSingleton<ICosmosDbService, CosmosDbService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Initialize Cosmos DB service during application startup
            var cosmosDbService = app.Services.GetRequiredService<ICosmosDbService>();
            cosmosDbService.Initialize();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
