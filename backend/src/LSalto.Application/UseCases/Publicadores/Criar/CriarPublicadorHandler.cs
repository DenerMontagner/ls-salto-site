using LSalto.Application.Common.Interfaces;
using LSalto.Domain.Entities;
using LSalto.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Application.UseCases.Publicadores.Criar;

public class CriarPublicadorHandler(IAppDbContext context, IPasswordService passwordService)
    : IRequestHandler<CriarPublicadorCommand, int>
{
    public async Task<int> Handle(CriarPublicadorCommand request, CancellationToken cancellationToken)
    {
        var emailJaExiste = await context.Publicadores
            .AnyAsync(p => p.EmailUsername == request.EmailUsername, cancellationToken);

        if (emailJaExiste)
            throw new InvalidOperationException($"Já existe um publicador com o email '{request.EmailUsername}'.");

        var publicador = new Publicador
        {
            Nome = request.Nome,
            EmailUsername = request.EmailUsername,
            SenhaHash = passwordService.Hash(request.Senha),
            Sexo = Enum.Parse<Sexo>(request.Sexo),
            DataNascimento = request.DataNascimento,
            DataBatismo = request.DataBatismo,
            Telefone = request.Telefone,
            Endereco = request.Endereco
        };

        context.Publicadores.Add(publicador);
        await context.SaveChangesAsync(cancellationToken);

        if (request.IdGrupo.HasValue)
        {
            context.GruposPublicadores.Add(new GrupoPublicador
            {
                IdGrupo = request.IdGrupo.Value,
                IdPublicador = publicador.Id
            });
            await context.SaveChangesAsync(cancellationToken);
        }

        return publicador.Id;
    }
}
