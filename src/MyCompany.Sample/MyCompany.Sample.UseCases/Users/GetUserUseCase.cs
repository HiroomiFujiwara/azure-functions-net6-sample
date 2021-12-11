namespace MyCompany.Sample.UseCases.Users
{
    /// <summary>
    /// ユーザーを作成するユースケース
    /// </summary>
    public class GetUserUseCase : IGetUserUseCase
    {
        /// <summary>
        /// ユースケースを実行する
        /// </summary>
        /// <param name="request">リクエストパラメーター</param>
        /// <returns>ユーザー取得レスポンス</returns>
        public async Task<GetUserResponse> ExecuteAsync(GetUserRequest request)
        {
            var user = new GetUserResponse(
                request.Id,
                request.IDaasId,
                "hoge",
                "HOGE");
            return await Task.FromResult(user);
        }
    }
}
