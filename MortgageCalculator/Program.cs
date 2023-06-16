using FluentValidation;
using MortgageCalculator.SharedLogic.Calculators;
using MortgageCalculator.SharedLogic.Models;
using Newtonsoft.Json.Serialization;
using FluentValidation.AspNetCore;

namespace MortgageCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}