using LSalto.Application.Common.Interfaces;
using LSalto.Domain.Entities;
using LSalto.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Application.UseCases.Publicadores.Atualizar;

public class AtualizarPublicadorHandler(IAppDbContext context)
    : IRequestHandler<AtualizarPublicadorCommand>
{
    public async Task Handle(AtualizarPublicadorCommand request, CancellationToken cancellationToken)
    {
        var publicador = await context.Publicadores
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Publicador {request.Id} não encontrado.");

        var emailEmUso = await context.Publicadores
            .AnyAsync(p => p.EmailUsername == request.EmailUsername && p.Id != request.Id, cancellationToken);

        if (emailEmUso)
            throw new InvalidOperationException($"Já existe um publicador com o email '{request.EmailUsername}'.");

        publicador.Nome = request.Nome;
        publicador.EmailUsername = request.EmailUsername;
        publicador.Sexo = Enum.Parse<Sexo>(request.Sexo);
        publicador.DataNascimento = request.DataNascimento;
        publicador.DataBatismo = request.DataBatismo;
        publicador.Telefone = request.Telefone;
        publicador.Endereco = request.Endereco;

        var grupoAtual = await context.GruposPublicadores
            .Where(gp => gp.IdPublicador == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (grupoAtual != null) context.GruposPublicadores.Remove(grupoAtual);

        if (request.IdGrupo.HasValue)
            context.GruposPublicadores.Add(new GrupoPublicador
            {
                IdGrupo = request.IdGrupo.Value,
                IdPublicador = request.Id
            });

        await context.SaveChangesAsync(cancellationToken);
    }
}
