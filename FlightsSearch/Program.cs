using Domain.Data;
using Domain.Interfaces;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;
using Services.Implementation;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMemoryCache();
builder.Services.AddLogging();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling =
            Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddDbContext<FlightSearchContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  // Update the connection string as needed

builder.Services.AddScoped<AmadeusTokenHelper>();
builder.Services.AddScoped<IFlightSearchRepository, FlightSearchRepository>();
builder.Services.AddScoped<IFlightSearchService, FlightSearchService>();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());
app.UseHttpsRedirection();
app.Run();
