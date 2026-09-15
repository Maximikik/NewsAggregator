using NewsAggregator.Application.Common.Interfaces;
using NewsAggregator.Application.Common.Models;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;

namespace NewsAggregator.Infrastructure.Rss;

public sealed partial class RssParser(
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
                            StripHtml(item.Title.Text),
                            StripHtml(item.Summary?.Text ?? ""),
                            item.Links
                                .FirstOrDefault()?
                                .Uri
                                .ToString() ?? "",
                            item.PublishDate.UtcDateTime,
                            categories);
                    })
                .ToList();
    }

    private static string StripHtml(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        var withoutTags = HtmlTagRegex().Replace(value, " ");

        var decoded = WebUtility.HtmlDecode(withoutTags);

        return WhitespaceRegex().Replace(decoded, " ").Trim();
    }

    [GeneratedRegex("<[^>]*>")]
    private static partial Regex HtmlTagRegex();

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();
}