namespace NewsAggregator.WebAPI.Contracts.Sources;

internal sealed record CreateSourceRequest(
    string Name,
    string BaseUrl);
