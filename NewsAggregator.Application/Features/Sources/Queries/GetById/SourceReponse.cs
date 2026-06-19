namespace NewsAggregator.Application.Features.Sources.Queries.GetById;

public sealed record SourceResponse(
  Guid Id,
  string Name,
  string BaseUrl);
