using LSalto.Application.Common.Interfaces;
using LSalto.Domain.Entities;
using LSalto.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Application.UseCases.Designacoes.Criar;

public class CriarDesignacaoHandler(IAppDbContext context) : IRequestHandler<CriarDesignacaoCommand, int>
{
    public async Task<int> Handle(CriarDesignacaoCommand request, CancellationToken cancellationToken)
    {
        var tipo = await context.TiposDesignacao
            .FirstOrDefaultAsync(t => t.Id == request.IdTipoDesignacao, cancellationToken)
            ?? throw new InvalidOperationException($"Tipo de designação '{request.IdTipoDesignacao}' não encontrado.");

        var publicador = await context.Publicadores
            .Include(p => p.Cargos)
            .FirstOrDefaultAsync(p => p.Id == request.IdPublicadorTitular, cancellationToken)
            ?? throw new InvalidOperationException($"Publicador '{request.IdPublicadorTitular}' não encontrado.");

        if (tipo.RequerSexoMasculino && publicador.Sexo != Sexo.Masculino)
            throw new InvalidOperationException("Este tipo de designação requer publicador do sexo masculino.");

        if (tipo.RequerCargoEspecifico != RequisitoCargo.Nenhum)
        {
            var temCargo = publicador.Cargos.Any(c =>
                c.DataFim == null &&
                (tipo.RequerCargoEspecifico == RequisitoCargo.Anciao
                    ? c.IdCargo == 1
                    : c.IdCargo == 1 || c.IdCargo == 2));

            if (!temCargo)
                throw new InvalidOperationException("Publicador não possui o cargo necessário para esta designação.");
        }

        var conflito = await context.Designacoes.AnyAsync(
            d => d.IdPublicadorTitular == request.IdPublicadorTitular && d.Data == request.Data,
            cancellationToken);

        if (conflito)
            throw new InvalidOperationException("Este publicador já possui uma designação nesta data.");

        var designacao = new Designacao
        {
            IdTipoDesignacao = request.IdTipoDesignacao,
            Data = request.Data,
            IdPublicadorTitular = request.IdPublicadorTitular,
            IdPublicadorAjudante = request.IdPublicadorAjudante,
            IdGrupo = request.IdGrupo
        };

        context.Designacoes.Add(designacao);
        await context.SaveChangesAsync(cancellationToken);
        return designacao.Id;
    }
}
