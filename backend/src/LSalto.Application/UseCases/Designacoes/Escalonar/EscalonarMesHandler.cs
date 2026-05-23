using LSalto.Application.Common.Interfaces;
using LSalto.Domain.Entities;
using LSalto.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Application.UseCases.Designacoes.Escalonar;

public class EscalonarMesHandler(IAppDbContext context) : IRequestHandler<EscalonarMesCommand, int>
{
    public async Task<int> Handle(EscalonarMesCommand request, CancellationToken cancellationToken)
    {
        var tiposIds = request.Designacoes.Select(d => d.IdTipoDesignacao).Distinct().ToList();
        var tipos = await context.TiposDesignacao
            .Where(t => tiposIds.Contains(t.Id))
            .ToListAsync(cancellationToken);

        var publicadoresIds = request.Designacoes.Select(d => d.IdPublicadorTitular).Distinct().ToList();
        var publicadores = await context.Publicadores
            .Include(p => p.Cargos)
            .Where(p => publicadoresIds.Contains(p.Id))
            .ToListAsync(cancellationToken);

        var inicioMes = new DateOnly(request.Ano, request.Mes, 1);
        var fimMes = inicioMes.AddMonths(1).AddDays(-1);
        var designacoesExistentes = await context.Designacoes
            .Where(d => d.Data >= inicioMes && d.Data <= fimMes)
            .ToListAsync(cancellationToken);

        var novasDesignacoes = new List<Designacao>();

        foreach (var item in request.Designacoes)
        {
            var tipo = tipos.FirstOrDefault(t => t.Id == item.IdTipoDesignacao)
                ?? throw new InvalidOperationException($"Tipo de designação '{item.IdTipoDesignacao}' não encontrado.");

            var publicador = publicadores.FirstOrDefault(p => p.Id == item.IdPublicadorTitular)
                ?? throw new InvalidOperationException($"Publicador '{item.IdPublicadorTitular}' não encontrado.");

            if (tipo.RequerSexoMasculino && publicador.Sexo != Sexo.Masculino)
                throw new InvalidOperationException(
                    $"Publicador '{publicador.Nome}' não pode ser designado como '{tipo.Nome}' (requer sexo masculino).");

            if (tipo.RequerCargoEspecifico != RequisitoCargo.Nenhum)
            {
                var temCargo = publicador.Cargos.Any(c =>
                    c.DataFim == null &&
                    (tipo.RequerCargoEspecifico == RequisitoCargo.Anciao
                        ? c.IdCargo == 1
                        : c.IdCargo == 1 || c.IdCargo == 2));

                if (!temCargo)
                    throw new InvalidOperationException(
                        $"Publicador '{publicador.Nome}' não possui o cargo necessário para '{tipo.Nome}'.");
            }

            var conflito = designacoesExistentes.Any(d => d.IdPublicadorTitular == item.IdPublicadorTitular && d.Data == item.Data)
                        || novasDesignacoes.Any(d => d.IdPublicadorTitular == item.IdPublicadorTitular && d.Data == item.Data);

            if (conflito)
                throw new InvalidOperationException(
                    $"Publicador '{publicador.Nome}' já possui uma designação em {item.Data:dd/MM/yyyy}.");

            novasDesignacoes.Add(new Designacao
            {
                IdTipoDesignacao = item.IdTipoDesignacao,
                Data = item.Data,
                IdPublicadorTitular = item.IdPublicadorTitular,
                IdPublicadorAjudante = item.IdPublicadorAjudante,
                IdGrupo = item.IdGrupo
            });
        }

        context.Designacoes.AddRange(novasDesignacoes);
        await context.SaveChangesAsync(cancellationToken);
        return novasDesignacoes.Count;
    }
}
