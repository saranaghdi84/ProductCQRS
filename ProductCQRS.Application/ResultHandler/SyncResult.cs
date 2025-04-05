namespace ProductCQRS.Application.ResultHandler;

public record SyncResult
{
    public bool IsSuccess { get; init; }
    public int UpsertedCount { get; init; }
    public int DeletedCount { get; init; }
    public List<string> Warnings { get; init; } = new();
    public string? Error { get; init; }

    public static SyncResult Success(int upserted, int deleted) => new()
    {
        IsSuccess = true,
        UpsertedCount = upserted,
        DeletedCount = deleted
    };

    public static SyncResult Failure(string error) => new()
    {
        IsSuccess = false,
        Error = error
    };

    public SyncResult WithWarning(string warning)
    {
        Warnings.Add(warning);
        return this;
    }
}
