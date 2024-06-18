
using System.Text.Json;
using TourOfPonies.Api.Data;
using TourOfPonies.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

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

app.Run();


