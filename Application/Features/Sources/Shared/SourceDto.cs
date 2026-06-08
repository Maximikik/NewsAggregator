namespace NewsAggregator.Application.Features.Sources.Shared;

public sealed record SourceDto(
    Guid Id,
    string Name,
    string BaseUrl);