using LSalto.Application.Common.Interfaces;
using LSalto.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Application.UseCases.Designacoes.ListPorMes;

public class ListDesignacoesPorMesHandler(IAppDbContext context)
    : IRequestHandler<ListDesignacoesPorMesQuery, List<DesignacaoDto>>
{
    public async Task<List<DesignacaoDto>> Handle(ListDesignacoesPorMesQuery request, CancellationToken cancellationToken)
    {
        var inicioMes = new DateOnly(request.Ano, request.Mes, 1);
        var fimMes = inicioMes.AddMonths(1).AddDays(-1);

        return await context.Designacoes
            .Include(d => d.TipoDesignacao)
            .Include(d => d.PublicadorTitular)
            .Include(d => d.PublicadorAjudante)
            .Include(d => d.Grupo)
            .Where(d => d.Data >= inicioMes && d.Data <= fimMes)
            .OrderBy(d => d.Data)
            .Select(d => new DesignacaoDto(
                d.Id,
                d.TipoDesignacao.Nome,
                d.Data,
                d.PublicadorTitular.Nome,
                d.PublicadorAjudante != null ? d.PublicadorAjudante.Nome : null,
                d.Grupo != null ? d.Grupo.Nome : null
            ))
            .ToListAsync(cancellationToken);
    }
}
