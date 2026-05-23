using LSalto.Application.Common.Interfaces;
using LSalto.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Application.UseCases.Auth.Login;

public class LoginHandler(IAppDbContext context, IPasswordService passwordService)
    : IRequestHandler<LoginCommand, LoginResultDto>
{
    public async Task<LoginResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var publicador = await context.Publicadores
            .Include(p => p.Cargos)
            .FirstOrDefaultAsync(p => p.EmailUsername == request.Email, cancellationToken)
            ?? throw new InvalidOperationException("Email ou senha inválidos.");

        if (!passwordService.Verify(request.Senha, publicador.SenhaHash))
            throw new InvalidOperationException("Email ou senha inválidos.");

        var isAnciao = publicador.Cargos.Any(c => c.DataFim == null && c.IdCargo == 1);
        var role = isAnciao ? "Anciao" : "Publicador";

        return new LoginResultDto(publicador.Id, publicador.Nome, role);
    }
}
