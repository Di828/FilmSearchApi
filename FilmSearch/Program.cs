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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IFilmService, FilmService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IActorService, ActorService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
