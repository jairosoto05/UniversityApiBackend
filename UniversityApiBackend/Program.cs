using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;

var builder = WebApplication.CreateBuilder(args);

string CONNECTIONNAME = "UniversityDB";
var ConnectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

// Add Context
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseOracle(ConnectionString, b =>
b.UseOracleSQLCompatibility("11")));

// Add services to the container.

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
