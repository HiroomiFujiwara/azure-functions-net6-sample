using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;

namespace MyCompany.Sample.Presentations.Functions.Users;

/// <summary>
/// ユーザー追加レスポンス
/// </summary>
[OpenApiExample(typeof(CreateUserResponseExample))]
public class CreateUserResponse
{
    /// <summary>
    /// ユーザーID
    /// </summary>
    [OpenApiProperty(Nullable = false, Description = "The user id.")]
    [Required]
    public Guid Id { get; set; }

    /// <summary>
    /// IDaaS ID
    /// </summary>
    [OpenApiProperty(Nullable = false, Description = "The IDaaS id.")]
    [Required]
    [MaxLength(256)]
    public string IdaasId { get; set; }
}

/// <summary>
/// ユーザー追加レスポンスの例
/// </summary>
public class CreateUserResponseExample : OpenApiExample<CreateUserResponse>
{
    override public IOpenApiExample<CreateUserResponse> Build(NamingStrategy namingStrategy = null)
    {
        Examples.Add(OpenApiExampleResolver.Resolve(
            "Example",
            new CreateUserResponse
            {
                Id = Guid.NewGuid(),
                IdaasId = "(Depends on IDaaS)"
            },
            namingStrategy));
        return this;
    }
}