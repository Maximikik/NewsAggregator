using NewsAggregator.Application.Common.Results;

namespace NewsAggregator.WebAPI.Extensions;

internal static class ResultExtensions
{
    internal static IResult ToHttpResult<T>(
        this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }

        return result.Error?.Code switch
        {
            "not_found" =>
                Results.NotFound(result.Error),

            "validation" =>
                Results.BadRequest(result.Error),

            "conflict" =>
                Results.Conflict(),

            _ =>
                Results.BadRequest(result.Error)
        };
    }

    internal static IResult ToHttpResult(
        this Result result)
    {
        if (result.IsSuccess)
        {
            return Results.Ok();
        }

        return result.Error?.Code switch
        {
            "not_found" =>
                Results.NotFound(result.Error),

            "validation" =>
                Results.BadRequest(result.Error),

            "conflict" =>
                Results.Conflict(),

            _ =>
                Results.BadRequest(result.Error)
        };
    }
}
