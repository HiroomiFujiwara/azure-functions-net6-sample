namespace MyCompany.Sample.UseCases.Users;

/// <summary>
/// ユーザー取得レスポンス
/// </summary>
public record GetUserResponse(
    Guid Id,
    string? IdaasId,
    string? FirstName,
    string? LastName)
{
}