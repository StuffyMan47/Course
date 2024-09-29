using System.Security.Cryptography;
using System.Text;
using Course.Application.Services;

namespace Course.Infrastructure.Services;

public class PasswordService : IPasswordService
{

    public (string passwordHash, string passwordSalt) GeneratePasswordData(string password)
    {
        password = password.Trim();

        using var hmac = new HMACSHA512();
        string passwordSalt = Convert.ToBase64String(hmac.Key);
        string passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));

        return (passwordHash, passwordSalt);

    }

    public bool VerifyPassword(string password, string passwordHash, string passwordSalt)
    {
        using var hmac = new HMACSHA512(Convert.FromBase64String(passwordSalt));
        byte[] passwordHashByteArray = Convert.FromBase64String(passwordHash);
        byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return !computedHash.Where((t, i) => t != passwordHashByteArray[i]).Any();

    }
}