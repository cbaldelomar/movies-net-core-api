using ErrorOr;

namespace Movies.Api.Extensions;

public static class ErrorExtensions
{
    public static IResult ToProblemDetails(this IErrorOr result)
    {
        if (!result.IsError)
        {
            throw new InvalidOperationException("Cannot convert a successful result to a problem details.");
        }

        var firstError = result.Errors![0];
        int statusCode = GetStatusCode(firstError.Type);

        return Results.Problem(
            statusCode: statusCode,
            //title: GetTitle(statusCode),
            //type: GetType(statusCode),
            detail: GetDetail(firstError),
            extensions: firstError.Type == ErrorType.Validation
                ? GetErrors(result.Errors)
                : null
            );
        //extensions: int statusCode new Dictionary<string, object?>
        //{
        //    { "errors", new [] { error.Errors } }
        //});

        static int GetStatusCode(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError,
            };

        //static string GetTitle(int statusCode) =>
        //    ReasonPhrases.GetReasonPhrase(statusCode);

        //static string GetType(int statusCode) =>
        //    $"https://httpstatuses.com/{statusCode}";

        static string GetDetail(Error error) =>
            error.Type switch
            {
                ErrorType.Validation => "The request produced one or more errors",
                _ => error.Description
            };

        static Dictionary<string, object?> GetErrors(List<Error> errors)
        {
            var propertyErrors = errors
                .GroupBy(e => e.Code)
                .ToDictionary(g => ToCamelCase(g.Key), g => g.Select(x => x.Description).ToArray());

            return new Dictionary<string, object?> { { "errors", new[] { propertyErrors } } };
            //return new Dictionary<string, object?> { { "errors", propertyErrors } };
        }

        static string ToCamelCase(string str)
        {
            if (string.IsNullOrEmpty(str) || char.IsLower(str[0]))
            {
                return str;
            }

            return char.ToLowerInvariant(str[0]) + str[1..];
        }
    }
}
