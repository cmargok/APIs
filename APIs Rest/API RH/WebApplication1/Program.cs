using DataContext;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using ADO.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddControllersAsServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RH_Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("rh_connection")));
builder.Services.AddSingleton<IAdoRepository>(connection => new AdoRepository(builder.Configuration.GetConnectionString("rh_connection")));


//AdoRepository(obje=> obje.)>();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

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
