using NewsAggregator.Application.Features.Articles.Commands.Create;
using NewsAggregator.Application.Features.Articles.Queries.GetAll;
using NewsAggregator.WebAPI.Contracts.Articles;

namespace NewsAggregator.WebAPI.Common.Mappings;

internal static class ArticleMappings
{
    internal static CreateArticleCommand ToCommand(
        this CreateArticleRequest request)
    {
        return new CreateArticleCommand(
            request.Title,
            request.Description,
            request.SourceId);
    }

    internal static GetAllArticlesQuery ToQuery(
        this GetAllArticlesRequest request)
    {
        return new GetAllArticlesQuery(
            request.PageNumber,
            request.PageSize);
    }
}
