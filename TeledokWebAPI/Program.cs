using Microsoft.EntityFrameworkCore;
using NLog;
using Persistence;
using Persistence.Repository;
using Persistence.Repository.Concrete;
using Services.LoggerService;
using TeledokWebAPI.Extensions;
using UseCases;
using UseCases.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigurationManager configuration = builder.Configuration;
LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("Persistence")));
builder.Services.ConfigureLoggerService();

builder.Services.AddScoped<IClientLogic, ClientLogic>();
builder.Services.AddScoped<IFounderLogic, FounderLogic>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IFounderRepository, FounderRepository>();

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

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
