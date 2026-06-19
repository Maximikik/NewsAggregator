using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Models;
using System.ServiceModel.Syndication;
using System.Xml;

namespace NewsAggregator.Infrastructure.Rss;

public sealed class RssParser(
    HttpClient httpClient)
    : IRssParser
{

    public async Task<IReadOnlyCollection<RssArticleModel>> ParseAsync(
        string url,
        CancellationToken cancellationToken)
    {
        using var stream =
            await httpClient.GetStreamAsync(
                url,
                cancellationToken);

        using var reader = XmlReader.Create(stream);

        var feed = SyndicationFeed.Load(reader);

        return feed.Items
                .Select(
                    item =>
                    {
                        var categories =
                            item.Categories
                                .Select(
                                    x => x.Name)
                                .Where(
                                    x => !string.IsNullOrWhiteSpace(x))
                                .Distinct()
                                .ToList();

                        return new RssArticleModel(
                            item.Title.Text,
                            item.Summary?.Text ?? "",
                            item.Links
                                .FirstOrDefault()?
                                .Uri
                                .ToString() ?? "",
                            item.PublishDate.UtcDateTime,
                            categories);
                    })
                .ToList();
    }
}