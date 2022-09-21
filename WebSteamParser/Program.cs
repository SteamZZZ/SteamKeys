using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebSteamParser.Models;

//IConfigurationRoot configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//    .AddUserSecrets(typeof(Program).GetTypeInfo().Assembly)
//    .Build();
//IServiceCollection services = new ServiceCollection();


//services.AddLogging(options =>
//{
//    options.AddDebug();
//    options.AddConsole();
//}
//);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<u1286676_STEAM_PARSINGContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SteamConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
