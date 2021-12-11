using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using MyCompany.Sample.Presentations.Functions.Validations;
using MyCompany.Sample.UseCases.Users;

namespace MyCompany.Sample.Presentations.Functions.Users;

/// <summary>
/// ユーザ追加ハンドラー
/// </summary>
public class CreateUserHandler
{
    /// <summary>
    /// ユーザー追加ユースケース
    /// </summary>
    private readonly ICreateUserUseCase _createUserUseCase;

    /// <summary>
    /// ロガー
    /// </summary>
    private readonly ILogger<CreateUserHandler> _logger;

    /// <summary>
    /// インスタンスを生成する
    /// </summary>
    /// <param name="createUserUseCase">ユーザー追加ユースケース</param
    /// <param name="logger">ロガー</param>
    public CreateUserHandler(
        ICreateUserUseCase createUserUseCase,
        ILogger<CreateUserHandler> logger)
    {
        _createUserUseCase = createUserUseCase;
        _logger = logger;
    }

    /// <summary>
    /// ユーザー追加を実行する
    /// </summary>
    /// <param name="req">HTTPリクエスト</param>
    /// <returns></returns>
    [OpenApiOperation(
        operationId: "users.create",
        tags: new[] { "Users" },
        Summary = "Create user.",
        Description = "This creates a user.",
        Visibility = OpenApiVisibilityType.Important)]
    [OpenApiRequestBody(
        "application/json; charset=utf-8",
        typeof(CreateUserRequest),
        Required = true)]
    [OpenApiResponseWithBody(
        HttpStatusCode.Created,
        "application/json; charset=utf-8",
        typeof(CreateUserResponse),
        Summary = "The response",
        Description = "This returns the response")]
    [Function("Users.Create")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users")]
        HttpRequestData req)
    {
        string idaasId = null;
        if (req.Headers.TryGetValues("X-MS-CLIENT-PRINCIPAL-ID", out var values))
            idaasId = values.FirstOrDefault();
#if DEBUG
#else
            if (idaasId is null)
                throw new InvalidOperationException("Not found IDaaS Id");
#endif
        CreateUserRequest request;
        try
        {
            var validateResult = await req.GetRequestBodyAsync<CreateUserRequest>();
            if (validateResult.IsValid is false)
            {
                // Hack: Write validation error message.
                _logger.LogWarning("Invalid request body.");
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            request = validateResult.Value;
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Invalid request body.");
            return req.CreateResponse(HttpStatusCode.BadRequest);
        }

        var useCaseResponse = await _createUserUseCase.ExecuteAsync(new UseCases.Users.CreateUserRequest(
            idaasId,
            request.FirstName,
            request.LastName));

        var response = req.CreateResponse();
        await response.WriteAsJsonAsync(CreateHttpResponseBody(useCaseResponse));
        response.StatusCode = HttpStatusCode.Created;
        return response;
    }

    /// <summary>
    /// HTTPレスポンス本文を生成する
    /// </summary>
    /// <param name="useCaseResponse">ユースケースレスポンス</param>
    /// <returns>HTTPレスポンス本文</returns>
    private static CreateUserResponse CreateHttpResponseBody(UseCases.Users.CreateUserResponse useCaseResponse) =>
        new()
        {
            Id = useCaseResponse.Id
        };
}