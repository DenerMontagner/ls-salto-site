using LSalto.Application.Common.Interfaces;
using LSalto.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Application.UseCases.Dashboard;

public class GetDashboardHandler(IAppDbContext context) : IRequestHandler<GetDashboardQuery, DashboardDto>
{
    public async Task<DashboardDto> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
    {
        var hoje = DateOnly.FromDateTime(DateTime.Today);

        var designacoes = await context.Designacoes
            .Include(d => d.TipoDesignacao)
            .Include(d => d.PublicadorTitular)
            .Include(d => d.PublicadorAjudante)
            .Include(d => d.Grupo)
            .Where(d => d.IdPublicadorTitular == request.IdPublicador && d.Data >= hoje)
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

        var anuncios = await context.Anuncios
            .OrderByDescending(a => a.DataCriacao)
            .Take(2)
            .Select(a => new AnuncioDto(a.Id, a.Descricao, a.DataCriacao))
            .ToListAsync(cancellationToken);

        return new DashboardDto(designacoes, anuncios);
    }
}
