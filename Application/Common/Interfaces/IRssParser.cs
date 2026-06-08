using NewsAggregator.Application.Common.Models;

namespace NewsAggregator.Application.Common.Interfaces;

public interface IRssParser
{
    Task<List<RssArticleModel>> ParseAsync(
        string url,
        CancellationToken cancellationToken);
}