namespace LSalto.Application.Common.Interfaces;

public interface IPasswordService
{
    string Hash(string plainText);
    bool Verify(string plainText, string hash);
}
