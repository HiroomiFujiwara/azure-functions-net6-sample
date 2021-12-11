namespace MyCompany.Sample.UseCases.Users;

/// <summary>
/// ユーザー追加ユースケース
/// </summary>
public interface ICreateUserUseCase
{
    /// <summary>
    /// ユースケースを実行する
    /// </summary>
    /// <param name="request">リクエストパラメーター</param>
    /// <returns>ユースケースレスポンス</returns>
    Task<CreateUserResponse> ExecuteAsync(CreateUserRequest request);
}