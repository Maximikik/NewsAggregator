using NewsAggregator.Application.Features.Sources.Create;
using NewsAggregator.WebAPI.Contracts.Sources;

namespace NewsAggregator.WebAPI.Common.Mappings;

internal static class SourceMappings
{
    internal static CreateSourceCommand ToCommand(
        this CreateSourceRequest request)
    {
        return new CreateSourceCommand(
            request.Name,
            request.BaseUrl);
    }
}
