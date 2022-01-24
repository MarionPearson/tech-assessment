using Microsoft.EntityFrameworkCore;
using SogetiAssessment.Contexts;
using SogetiAssessment.DataServices;
using SogetiAssessment.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDataService<Order>, OrderDataService>(
    x => new OrderDataService(
        new OrderContextWrapper(
            new DbContextOptionsBuilder<OrderContext>().UseInMemoryDatabase(databaseName: "PrimarySet").Options
        )
    )
);


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