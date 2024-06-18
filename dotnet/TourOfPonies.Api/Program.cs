
using System.Text.Json;
using TourOfPonies.Api;
using TourOfPonies.Api.Data;
using TourOfPonies.Api.Endpoints;
using TourOfPonies.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables()
	.AddUserSecrets<Program>()
	.AddCommandLine(args)
	.Build();



var configSection = builder.Configuration.GetSection("StorageSettings");
var storageSettings = new StorageSettings();
configSection.Bind(storageSettings);

builder.Services.AddSingleton<StorageSettings>(storageSettings);

builder.Services.AddLogging();

builder.Services.AddDependencies();


builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
					  builder =>
					  {
						  builder.AllowAnyOrigin()
						   .AllowAnyMethod()
						   .AllowAnyHeader();

					  });
});

// Add services to the container.
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
app.UseStaticFiles();
app.UseDefaultFiles();
app.UseCors();

app.MapPatch("/seed", async (TableStorageSeed seed) =>
{
	return await seed.Seed();
});

app.AddPoniesEndpoints();
app.Run();


