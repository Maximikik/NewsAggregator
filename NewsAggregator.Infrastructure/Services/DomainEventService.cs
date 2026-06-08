//using Mediator;
//using NewsAggregator.Application.Common.Interfaces;
//using NewsAggregator.Application.Common.Models;
//using NewsAggregator.Domain.Common;

//namespace NewsAggregator.Infrastructure.Services;

//public sealed class DomainEventService(IPublisher _mediator) : IDomainEventService
//{
//    public ValueTask Publish(DomainEvent domainEvent)
//    {
//        return _mediator.Publish(GetNotificationCoreespondingToDomainEvent(domainEvent));
//    }

//    private static INotification GetNotificationCoreespondingToDomainEvent(DomainEvent domainEvent)
//    {
//        return (INotification)Activator.CreateInstance(
//            typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent)!;
//    }
//}
