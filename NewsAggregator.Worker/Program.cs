using NewsAggregator.Application;
using NewsAggregator.Infrastructure;
using NewsAggregator.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplication();
builder.Services.AddWorkerInfrastructure(builder.Configuration);

builder.Services.AddHostedService<ArticleImportConsumer>();

var host = builder.Build();
host.Run();