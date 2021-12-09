using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using MyCompany.Sample.UseCases.Users;

namespace MyCompany.Sample.Presentations.Functions.Users
{
    /// <summary>
    /// ユーザー取得ハンドラー
    /// </summary>
    public class GetUserHandler
    {
        /// <summary>
        /// ロガー
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// ユーザー取得ユースケース
        /// </summary>
        private readonly IGetUserUseCase _getUserUseCase;

        /// <summary>
        /// インスタンスを生成する
        /// </summary>
        /// <param name="loggerFactory">ロガー</param>
        /// <param name="getUserUseCase">ユーザー取得ユースケース</param>
        public GetUserHandler(
            ILoggerFactory loggerFactory,
            IGetUserUseCase getUserUseCase)
        {
            _logger = loggerFactory.CreateLogger<GetUserHandler>();
            _getUserUseCase = getUserUseCase;
        }

        [OpenApiOperation(
            operationId: "GetUser",
            tags: new[] { "GetUser" },
            Summary = "Get user.",
            Description = "This gets a user.",
            Visibility = OpenApiVisibilityType.Important)]
        //[OpenApiSecurity(
        //    "function_key",
        //    Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        //    Name = "code",
        //    In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(
            HttpStatusCode.OK,
            "application/json; charset=utf-8",
            typeof(GetUserResponse),
            Summary = "The response",
            Description = "This returns the response")]
        [Function("Users.Get")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")]
            HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var id = req.Headers.GetValues("X-MS-CLIENT-PRINCIPAL-ID")?.FirstOrDefault();
            var useCaseResponse = await _getUserUseCase.ExecuteAsync(new GetUserRequest(id));

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(new GetUserResponse
            {
                Id = useCaseResponse.Id,
                FirstName = useCaseResponse.FirstName,
                LastName = useCaseResponse.LastName,
            });

            return response;
        }
    }
}
