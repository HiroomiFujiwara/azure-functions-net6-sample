namespace MyCompany.Sample.UseCases.Users;

/// <summary>
/// ユーザー追加レスポンス
/// </summary>
/// <param name="Id">ユーザーID</param>
public record CreateUserResponse(
    Guid Id)
{
}