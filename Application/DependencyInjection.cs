using FluentValidation;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using NewsAggregator.Application.Common.Behaviors;
using NewsAggregator.Application.Common.Behaviours;
using NewsAggregator.Application.Common.Interfaces;

namespace NewsAggregator.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator(options =>
        {
            options.Assemblies = [typeof(INewsAggregatorDbContext).Assembly];
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        services.AddTransient(typeof(IPipelineBehavior<,>),
           typeof(ValidationBehavior<,>));
        services.AddSingleton(
            typeof(IPipelineBehavior<,>), typeof(ErrorLoggingBehaviour<,>));
        services.AddTransient(
            typeof(IPipelineBehavior<,>), typeof(CacheBehavior<,>));

        services.Scan(scan => scan
           .FromAssemblyOf<INewsAggregatorDbContext>()
           .AddClasses(classes =>
               classes.AssignableTo(typeof(IDomainEventHandler<>)))
           .AsImplementedInterfaces()
           .WithScopedLifetime());

        return services;
    }
}
