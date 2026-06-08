namespace NewsAggregator.WebAPI.Contracts.Sources;

public sealed record CreateSourceRequest(
    string Name,
    string BaseUrl);
