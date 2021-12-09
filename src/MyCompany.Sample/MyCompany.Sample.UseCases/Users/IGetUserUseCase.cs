
namespace MyCompany.Sample.UseCases.Users
{
    /// <summary>
    /// ユーザー取得ユースケース
    /// </summary>
    public interface IGetUserUseCase
    {
        /// <summary>
        /// ユースケースを実行する
        /// </summary>
        /// <param name="request">リクエストパラメーター</param>
        /// <returns>ユーザー取得レスポンス</returns>
        Task<GetUserResponse> ExecuteAsync(GetUserRequest request);
    }
}