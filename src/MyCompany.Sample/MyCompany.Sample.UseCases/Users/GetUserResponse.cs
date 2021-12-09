namespace MyCompany.Sample.UseCases.Users
{
    /// <summary>
    /// ユーザー取得レスポンス
    /// </summary>
    public record GetUserResponse(
        string? Id,
        string? FirstName,
        string? LastName)
    {
    }
}
