namespace NewsAggregator.Application.Common.Results;

public static class Errors
{
    public static Error NotFound(
        string entityName)
        => new(
            "not_found",
            $"{entityName} was not found");

    public static Error Validation(
        string message)
        => new(
            "validation",
            message);

    public static Error Conflict(
        string message)
        => new(
            "conflict",
            message);
}

public static class UserErrors
{
    public static readonly Error AlreadyExists =
        new(
            "Users.AlreadyExists",
            "User already exists");

    public static readonly Error InvalidCredentials =
        new(
            "Users.InvalidCredentials",
            "Invalid credentials");
}