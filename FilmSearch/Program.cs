global using FilmSearch.Models;
global using FilmSearch.Dtos.FilmD;
global using FilmSearch.Dtos.ActorD;
global using FilmSearch.Dtos.ReviewD;
global using AutoMapper;
global using FilmSearch.Data;
using FilmSearch.Services.FilmService;
using Microsoft.EntityFrameworkCore;
using FilmSearch.Services.ReviewService;
using FilmSearch.Services.ActorService;
using LoggerService;
using Microsoft.AspNetCore.Diagnostics;
using FilmSearch.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

NLog.LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IActorService, ActorService>();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
