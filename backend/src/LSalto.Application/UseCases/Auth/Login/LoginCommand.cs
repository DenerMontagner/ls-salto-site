using LSalto.Application.DTOs;
using MediatR;

namespace LSalto.Application.UseCases.Auth.Login;

public record LoginCommand(string Email, string Senha) : IRequest<LoginResultDto>;
