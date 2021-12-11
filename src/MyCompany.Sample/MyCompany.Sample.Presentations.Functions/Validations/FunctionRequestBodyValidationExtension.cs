using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;

namespace MyCompany.Sample.Presentations.Functions.Validations;

/// <summary>
/// リクエスト本文
/// </summary>
/// <typeparam name="T">リクエスト本文の型</typeparam>
/// <param name="IsValid">リクエスト本文が妥当かどうか</param>
/// <param name="Value">リクエスト本文の値</param>
/// <param name="ValidationResults">バリデーション結果</param>
public record RequestBody<T>(
    bool IsValid,
    T Value,
    IReadOnlyList<ValidationResult> ValidationResults)
{
    /// <summary>
    /// リクエスト本文が妥当かどうか
    /// </summary>
    public bool IsValid { get; } = IsValid;

    /// <summary>
    /// リクエスト本文の値
    /// </summary>
    public T Value { get; } = Value;

    /// <summary>
    /// バリデーション結果
    /// </summary>
    public IReadOnlyList<ValidationResult> ValidationResults { get; } = ValidationResults;
}

/// <summary>
/// Azure Functionsのリクエスト本文のバリデーション拡張メソッド
/// </summary>
public static class FunctionRequestBodyValidationExtension
{
    /// <summary>
    /// Azure Functionsのリクエスト本文を取得する
    /// </summary>
    /// <typeparam name="T">リクエスト本文の型</typeparam>
    /// <param name="requestData">リクエスト本文</param>
    /// <returns>リクエスト本文</returns>
    public static async Task<RequestBody<T>> GetRequestBodyAsync<T>(this HttpRequestData requestData)
    {
        var value = await requestData.ReadFromJsonAsync<T>();

        var validationResult = new List<ValidationResult>();
        var isValid = value != null && Validator.TryValidateObject(
            value,
            new ValidationContext(value, null, null),
            validationResult,
            true);

        return new(isValid, value, validationResult);
    }
}