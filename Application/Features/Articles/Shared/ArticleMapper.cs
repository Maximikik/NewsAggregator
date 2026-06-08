using NewsAggregator.Application.Features.Articles.Queries.GetArticleById;
using NewsAggregator.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace NewsAggregator.Application.Features.Articles.Shared;

[Mapper]
public static partial class ArticleMapper
{
    public static partial ArticleResponse ToResponse(Article article);
}