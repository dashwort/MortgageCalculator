using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FluentValidation.AspNetCore;
using MortgageCalculator.SharedLogic.Calculators;
using MortgageCalculator.SharedLogic.Models;
using Newtonsoft.Json.Serialization;

namespace MortgageCalculatorWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddTransient<IMonthlyMortgage, MonthlyMortgage>();
            builder.Services.AddTransient<IValidator, ReceivedMortgageDataValidationModel>();

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.MapRazorPages();

            app.Run();
        }
    }
}