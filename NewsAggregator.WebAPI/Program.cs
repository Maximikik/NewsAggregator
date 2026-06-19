using Hangfire;
using Microsoft.EntityFrameworkCore;
using NewsAggregator.Application;
using NewsAggregator.Infrastructure;
using NewsAggregator.Infrastructure.BackgroundJobs;
using NewsAggregator.WebAPI.Common.Helpers;
using NewsAggregator.WebAPI.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddJwt(builder.Configuration);
builder.Services.AddConfiguredCors(builder.Configuration);
builder.Services.AddConfiguredSwagger();

builder.Services.AddHangfireServer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<NewsAggregatorDbContext>();
    db.Database.Migrate();
}

app.UseHangfireDashboard();

app.UseCors("Default");

app.UseHttpsRedirection();

app.MapEndpoints();

app.Services.RegisterRecurringJobs();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
