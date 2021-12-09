namespace MyCompany.Sample.Presentations.Functions.Users
{
    /// <summary>
    /// ユーザー取得レスポンス
    /// </summary>
    public class GetUserResponse
    {
        /// <summary>
        /// ユーザーID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// ファーストネーム
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// ラストネーム
        /// </summary>
        public string LastName { get; set; }
    }
}
