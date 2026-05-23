using LSalto.Application.Common.Interfaces;
using LSalto.Application.DTOs;
using LSalto.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Application.UseCases.Publicadores.ListPorTipo;

public class ListPublicadoresPorTipoHandler(IAppDbContext context)
    : IRequestHandler<ListPublicadoresPorTipoQuery, List<PublicadorResumoDto>>
{
    public async Task<List<PublicadorResumoDto>> Handle(ListPublicadoresPorTipoQuery request, CancellationToken cancellationToken)
    {
        var tipo = await context.TiposDesignacao
            .FirstOrDefaultAsync(t => t.Id == request.IdTipoDesignacao, cancellationToken)
            ?? throw new InvalidOperationException($"Tipo de designação '{request.IdTipoDesignacao}' não encontrado.");

        var query = context.Publicadores.Include(p => p.Cargos).AsQueryable();

        if (tipo.RequerSexoMasculino)
            query = query.Where(p => p.Sexo == Sexo.Masculino);

        if (tipo.RequerCargoEspecifico == RequisitoCargo.Anciao)
            query = query.Where(p => p.Cargos.Any(c => c.DataFim == null && c.IdCargo == 1));
        else if (tipo.RequerCargoEspecifico == RequisitoCargo.ServoOuAnciao)
            query = query.Where(p => p.Cargos.Any(c => c.DataFim == null && (c.IdCargo == 1 || c.IdCargo == 2)));

        return await query
            .OrderBy(p => p.Nome)
            .Select(p => new PublicadorResumoDto(p.Id, p.Nome))
            .ToListAsync(cancellationToken);
    }
}
