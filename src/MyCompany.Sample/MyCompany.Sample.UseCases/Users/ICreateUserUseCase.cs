namespace MyCompany.Sample.UseCases.Users;

/// <summary>
/// ���[�U�[�ǉ����[�X�P�[�X
/// </summary>
public interface ICreateUserUseCase
{
    /// <summary>
    /// ���[�X�P�[�X�����s����
    /// </summary>
    /// <param name="request">���N�G�X�g�p�����[�^�[</param>
    /// <returns>���[�X�P�[�X���X�|���X</returns>
    Task<CreateUserResponse> ExecuteAsync(CreateUserRequest request);
}