
using Application.Interfaces;
using Application.Services.Implamentations;
using Infrastructure.Context;
using Infrastructure.Repository;
using Infrastructure.Repository.Implementations;
using Infrastructure.Repository.Interfaces;
using Infrastructure.UnitofWork;
using Microsoft.AspNetCore.Connections;

namespace XVerify.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<Infrastructure.Context.IConnectionFactory>(_ =>
            new SqlConnectionFactory(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IInvoiceService, InvoiceService>();
            builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();


            builder.Services.AddControllers();
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
