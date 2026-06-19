using NewsAggregator.Application.Features.Sources.Queries.GetById;

namespace NewsAggregator.Application.Features.Sources.Queries.GetAll;

public record SourcesReponse(
    IEnumerable<SourceResponse> sources);