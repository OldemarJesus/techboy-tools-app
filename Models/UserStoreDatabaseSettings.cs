namespace web.Models;

public class UserStoreDatabaseSettings
{
    public string? ConnectionString { get; set; } = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
    public string? DatabaseName { get; set; } = Environment.GetEnvironmentVariable("DATABASE_NAME");
    public string? UsersCollectionName {get; set; } = Environment.GetEnvironmentVariable("DATABASE_USER_COLLECTION_NAME");
}
