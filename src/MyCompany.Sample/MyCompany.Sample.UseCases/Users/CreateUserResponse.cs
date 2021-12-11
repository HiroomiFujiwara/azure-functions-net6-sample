namespace MyCompany.Sample.UseCases.Users;

/// <summary>
/// ユーザー追加レスポンス
/// </summary>
/// <param name="Id"></param>
/// <param name="IdaasId"></param>
public record CreateUserResponse(
    Guid Id,
    string? IdaasId)
{
}