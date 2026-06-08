using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application;
using NewsAggregator.Infrastructure;
using NewsAggregator.WebAPI.Common.Helpers;
using NewsAggregator.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddJwt(builder.Configuration);
builder.Services.AddConfiguredCors(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UsePathBase("/swagger/index.html");

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<NewsAggregatorDbContext>();
    db.Database.Migrate();
}

app.UseCors("Default");

app.UseHttpsRedirection();

app.MapEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
