namespace MyCompany.Sample.UseCases.Users;

public record CreateUserRequest(
    string IdaasId,
    string FirstName,
    string LastName)
{
}