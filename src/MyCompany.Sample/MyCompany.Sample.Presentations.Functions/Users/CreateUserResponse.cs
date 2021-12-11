using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json;
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
    [JsonProperty(Required = Required.Always)]
    [Required]
    public Guid Id { get; set; }
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
                Id = Guid.NewGuid()
            },
            namingStrategy));
        return this;
    }
}