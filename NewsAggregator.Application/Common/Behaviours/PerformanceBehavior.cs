using Mediator;
using Microsoft.Extensions.Logging;
using NewsAggregator.Application.Common.Caching;
using System.Diagnostics;

namespace NewsAggregator.Application.Common.Behaviours;

internal class PerformanceBehavior<TMessage, TResponse>(
    ILogger<PerformanceBehavior<TMessage, TResponse>> logger)
    : IPipelineBehavior<TMessage, TResponse>
    where TMessage : notnull, IMessage
{
    public async ValueTask<TResponse> Handle(TMessage message, MessageHandlerDelegate<TMessage, TResponse> next, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            return await next(message, cancellationToken);
        }
        finally
        {
            stopwatch.Stop();

            if (stopwatch.ElapsedMilliseconds > 1000)
            {
                logger.LogWarning(
                    "{Request} was slow. Took {Elapsed} ms",
                    typeof(TMessage).Name,
                    stopwatch.ElapsedMilliseconds);
            }
            else
            {
                logger.LogInformation(
                    "{Request} executed in {Elapsed} ms",
                    typeof(TMessage).Name,
                    stopwatch.ElapsedMilliseconds);
            }
        }
    }
}