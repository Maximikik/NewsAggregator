using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Models;
using System.ServiceModel.Syndication;
using System.Xml;

namespace NewsAggregator.Infrastructure.Rss;

public sealed class RssParser(
    HttpClient httpClient)
    : IRssParser
{

    public async Task<List<RssArticleModel>> ParseAsync(
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
            .Select(x => new RssArticleModel(
                x.Title.Text,
                x.Summary?.Text ?? "",
                x.Links.FirstOrDefault()?.Uri.ToString() ?? "",
                x.PublishDate.UtcDateTime))
            .ToList();
    }
}