using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MyCompany.Sample.Presentations.Functions.Users;

/// <summary>
/// ユーザー取得レスポンス
/// </summary>
[OpenApiExample(typeof(GetUserResponseExample))]
public class GetUserResponse
{
    /// <summary>
    /// ユーザーID
    /// </summary>
    [OpenApiProperty(Nullable = false, Description = "The user id.")]
    [JsonProperty(Required = Required.Always)]
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// IDaaS ID
    /// </summary>
    [OpenApiProperty(Nullable = false, Description = "The IDaaS id.")]
    [JsonProperty(Required = Required.Always)]
    [Required]
    [MaxLength(256)]
    public string IdaasId { get; set; }

    /// <summary>
    /// ファーストネーム
    /// </summary>
    [OpenApiProperty(Nullable = false, Description = "The first name.")]
    [JsonProperty(Required = Required.Always)]
    [Required]
    [RegularExpression(@"^[0-9a-zA-Z]*$")]
    [MaxLength(64)]
    public string FirstName { get; set; }

    /// <summary>
    /// ラストネーム
    /// </summary>
    [OpenApiProperty(Nullable = false, Description = "The last name.")]
    [JsonProperty(Required = Required.Always)]
    [Required]
    [RegularExpression(@"^[0-9a-zA-Z]*$")]
    [MaxLength(64)]
    public string LastName { get; set; }
}

/// <summary>
/// ユーザー取得レスポンスの例
/// </summary>
public class GetUserResponseExample : OpenApiExample<GetUserResponse>
{
    override public IOpenApiExample<GetUserResponse> Build(NamingStrategy namingStrategy = null)
    {
        Examples.Add(OpenApiExampleResolver.Resolve(
            "Example",
            new GetUserResponse
            {
                Id = Guid.NewGuid(),
                IdaasId = "(Depends on IDaaS)",
                FirstName = "Jiro",
                LastName = "Suzuki"
            },
            namingStrategy));
        return this;
    }
}