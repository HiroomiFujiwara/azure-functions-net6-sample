using System;

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
        public Guid Id { get; set; }

        /// <summary>
        /// IDaaS ID
        /// </summary>
        public string IdaasId { get; set; }

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
