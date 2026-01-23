using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApplication14.Command;
using WebApplication14.Models;
using WebApplication14.Query;
using WebApplication14.Repository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddDbContext<GotDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GotDB")));

builder.Services.AddScoped<IBattleQuery, BattleQuery>();
builder.Services.AddScoped<IBattleRepository, BattleRepository>();
builder.Services.AddScoped<IbattleCommand, BattleCommand>();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

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
