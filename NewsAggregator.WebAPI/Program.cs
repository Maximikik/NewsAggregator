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

builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

builder.Services.AddJwt(builder.Configuration);
builder.Services.AddConfiguredCors(builder.Configuration);
builder.Services.AddConfiguredSwagger();

builder.Services.AddHangfireServer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();

    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<NewsAggregatorDbContext>();
    db.Database.Migrate();
}


app.UseCors("Default");

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();

app.Services.RegisterRecurringJobs();

app.Run();