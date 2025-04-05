using ProductCQRS.Application.ResultHandler.LocalizationResources;

namespace ProductCQRS.Application.ResultHandler;

public sealed record Error (string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    // Factory methods with Persian localization
    public static Error NotFound(string entityType, object id) => new(
        "not.found",
        LocalizedMessages.Get($"{entityType}.NotFound", id));

    public static Error Validation(string field, string rule) => new(
        "validation",
        LocalizedMessages.Get($"Validation.{rule}", field));

    public static Error Conflict(string entityType) => new(
        "conflict",
        LocalizedMessages.Get($"{entityType}.Conflict"));
}
