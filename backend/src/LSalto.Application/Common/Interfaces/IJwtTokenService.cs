namespace LSalto.Application.Common.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(int id, string nome, string role);
}
