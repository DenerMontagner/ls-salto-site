using LSalto.Application.Common.Interfaces;
using LSalto.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Application.UseCases.Grupos.ListGrupos;

public class ListGruposHandler(IAppDbContext context) : IRequestHandler<ListGruposQuery, List<GrupoDto>>
{
    public async Task<List<GrupoDto>> Handle(ListGruposQuery request, CancellationToken cancellationToken)
    {
        return await context.Grupos
            .Include(g => g.AnciaoResponsavel)
            .OrderBy(g => g.Nome)
            .Select(g => new GrupoDto(
                g.Id,
                g.Nome,
                g.Local,
                g.AnciaoResponsavel != null ? g.AnciaoResponsavel.Nome : null
            ))
            .ToListAsync(cancellationToken);
    }
}
