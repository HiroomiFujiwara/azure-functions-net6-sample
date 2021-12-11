namespace MyCompany.Sample.UseCases.Users;

/// <summary>
/// ユーザー追加リクエスト
/// </summary>
/// <param name="IdaasId">IDaaS ID</param>
/// <param name="FirstName">ファーストネーム</param>
/// <param name="LastName">ラストネーム</param>
public record CreateUserRequest(
    string IdaasId,
    string FirstName,
    string LastName)
{
}