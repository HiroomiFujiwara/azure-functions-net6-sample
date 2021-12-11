using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MyCompany.Sample.Presentations.Functions.Users;

/// <summary>
/// ユーザー追加リクエスト
/// </summary>
[OpenApiExample(typeof(CreateUserRequestExample))]
public class CreateUserRequest
{
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
/// ユーザー追加リクエストの例
/// </summary>
public class CreateUserRequestExample : OpenApiExample<CreateUserRequest>
{
    override public IOpenApiExample<CreateUserRequest> Build(NamingStrategy namingStrategy = null)
    {
        Examples.Add(OpenApiExampleResolver.Resolve(
            "Example",
            new CreateUserRequest
            {
                FirstName = "Jiro",
                LastName = "Suzuki"
            },
            namingStrategy));
        return this;
    }
}