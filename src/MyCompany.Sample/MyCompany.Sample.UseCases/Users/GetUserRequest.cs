namespace MyCompany.Sample.UseCases.Users;

/// <summary>
/// ユーザー作成リクエストパラメーター
/// </summary>
/// <param name="Id">ユーザーID</param>
/// <param name="IDaasId">IDaaS ID</param>
public record GetUserRequest (
    Guid Id,
    string? IDaasId)
{
}