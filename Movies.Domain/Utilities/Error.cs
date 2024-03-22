namespace Movies.Domain.Utilities;

public record Error(ErrorType Type, string Code, string? Description = null)
{
    public static readonly Error None = new(ErrorType.None, string.Empty);

    public static ValidationError Validation(string key, string code, string description) =>
        new(key, code, description);

    public static ValidationError Validation(string code, string description) =>
        new(string.Empty, code, description);

    public static Error NotFound(string code, string description) =>
        new(ErrorType.NotFound, code, description);

    public static Error Forbidden(string code, string description) =>
        new(ErrorType.Forbidden, code, description);

    public static Error Internal(string code, string description) =>
        new(ErrorType.Internal, code, description);

    public static implicit operator Result(Error error) => Result.Failure(error);
}

public record ValidationError : Error
{
    public string Key { get; init; }

    public ValidationError(string key, string code, string description)
        : base(ErrorType.Validation, code, description)
    {
        Key = key;
    }
}