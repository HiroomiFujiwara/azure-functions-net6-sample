using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;
using MyCompany.Sample.UseCases.Users;

namespace MyCompany.Sample.Presentations.Functions.Users;

/// <summary>
/// ユーザー取得ハンドラー
/// </summary>
public class GetUserHandler
{
    /// <summary>
    /// ユーザー取得ユースケース
    /// </summary>
    private readonly IGetUserUseCase _getUserUseCase;

    /// <summary>
    /// インスタンスを生成する
    /// </summary>
    /// <param name="getUserUseCase">ユーザー取得ユースケース</param>
    public GetUserHandler(IGetUserUseCase getUserUseCase)
    {
        _getUserUseCase = getUserUseCase;
    }

    /// <summary>
    /// ユーザー取得を実行する
    /// </summary>
    /// <param name="req">HTTPリクエスト</param>
    /// <param name="id">ユーザーID</param>
    /// <returns></returns>
    [OpenApiOperation(
        operationId: "Users.Get",
        tags: new[] { "Users" },
        Summary = "Get user.",
        Description = "This gets a user.",
        Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter(
        "id",
        Type = typeof(Guid),
        In = ParameterLocation.Path,
        Required = true,
        Summary = "User Id.",
        Description = "This means a user Id.")]
    [OpenApiResponseWithBody(
        HttpStatusCode.OK,
        "application/json; charset=utf-8",
        typeof(GetUserResponse),
        Summary = "The response",
        Description = "This returns the response")]
    [Function("Users.Get")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{id:guid}")]
        HttpRequestData req,
        Guid id)
    {
        string idaasId = null;
        if (req.Headers.TryGetValues("X-MS-CLIENT-PRINCIPAL-ID", out var values))
            idaasId = values.FirstOrDefault();
#if DEBUG
#else
            if (idaasId is null)
                throw new InvalidOperationException("Not found IDaaS Id");
#endif

        var useCaseResponse = await _getUserUseCase.ExecuteAsync(new GetUserRequest(id, idaasId));

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(CreateHttpResponseBody(useCaseResponse));
        return response;
    }

    /// <summary>
    /// HTTPレスポンス本文を生成する
    /// </summary>
    /// <param name="useCaseResponse">ユースケースレスポンス</param>
    /// <returns>HTTPレスポンス本文</returns>
    private static GetUserResponse CreateHttpResponseBody(UseCases.Users.GetUserResponse useCaseResponse) => new()
    {
        Id = useCaseResponse.Id,
        IdaasId = useCaseResponse.IdaasId,
        FirstName = useCaseResponse.FirstName,
        LastName = useCaseResponse.LastName,
    };
}