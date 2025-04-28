using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Persistence;
using Backend.Service;
using Backend.Data;
using Backend.Persistence.Interfaces;
using Backend.ServiceHttp;

namespace Backend
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();

            services.AddScoped<IUserPersistence, UserPersistence>();
            services.AddScoped<UserService>();
            services.AddScoped<ICarSpecPersistence, CarSpecPersistence>();
            services.AddScoped<CarSpecService>();
            services.AddScoped<ICarPersistence, CarPersistence>();
            services.AddScoped<CarService>();
            services.AddScoped<IAuctionPersistence, AuctionPersistence>();
            services.AddScoped<AuctionService>();
            services.AddScoped<IUserInteractionPersistence, UserInteractionPersistence>();
            services.AddScoped<UserInteractionService>();

            services.AddHttpClient<RecommendationService>(client =>
            {
                client.BaseAddress = new Uri("http://localhost:8000/");
            });


            services.AddScoped<DataSeeder>();

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()   // Allow requests from any origin
                               .AllowAnyMethod()   // Allow any HTTP method (GET, POST, etc.)
                               .AllowAnyHeader();  // Allow any headers
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        { 
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
                // seeder.Seed();
                //seeder.SeedCarSpecs();
                //seeder.SeedCars();
                //seeder.SeedAuctions();
                seeder.SeedUserInteractions();

                // TEST your RecommendationClient here
                //var recommendationClient = scope.ServiceProvider.GetRequiredService<RecommendationService>();
                //var recommendations = recommendationClient.GetRecommendationsAsync(1).GetAwaiter().GetResult();
                //// print all recomandation
                //foreach (var recommendation in recommendations)
                //{
                //    Console.WriteLine($"Recommendation: {recommendation.Genmodel}");
                //}
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("AllowAll"); // Apply the CORS policy

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
