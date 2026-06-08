namespace NewsAggregator.WebAPI.Common.Options;

public sealed class CorsPolicyOptions
{
    public const string SectionName = "Cors";

    public string[] AllowedOrigins { get; init; } = [];
}