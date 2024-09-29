namespace Course.Application.Services;

public interface IPasswordService
{
    (string passwordHash, string passwordSalt) GeneratePasswordData(string password);
    bool VerifyPassword(string password, string passwordHash, string passwordSalt);
}