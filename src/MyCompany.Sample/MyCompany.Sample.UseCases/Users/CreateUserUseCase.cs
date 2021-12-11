namespace MyCompany.Sample.UseCases.Users;

/// <summary>
/// ユーザー追加ユースケース
/// </summary>
public class CreateUserUseCase : ICreateUserUseCase
{
    /// <summary>
    /// ユースケースを実行する
    /// </summary>
    /// <param name="request">リクエストパラメーター</param>
    /// <returns>ユースケースレスポンス</returns>
    public async Task<CreateUserResponse> ExecuteAsync(CreateUserRequest request)
    {
        return await Task.FromResult(new CreateUserResponse(
            Guid.NewGuid(),
            request.IdaasId));
    }
}