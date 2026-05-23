using LSalto.Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LSalto.Infrastructure.Services;

public class PasswordService : IPasswordService
{
    private readonly PasswordHasher<string> _hasher = new();

    public string Hash(string plainText) =>
        _hasher.HashPassword(string.Empty, plainText);

    public bool Verify(string plainText, string hash)
    {
        var result = _hasher.VerifyHashedPassword(string.Empty, hash, plainText);
        return result != PasswordVerificationResult.Failed;
    }
}
