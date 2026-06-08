namespace NewsAggregator.Application.Features.Categories.Queries.GetById;

public sealed record CategoryResponse(
  Guid Id,
  string Name);
