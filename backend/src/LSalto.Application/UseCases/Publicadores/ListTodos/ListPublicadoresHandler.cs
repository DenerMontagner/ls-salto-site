using LSalto.Application.Common.Interfaces;
using LSalto.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Application.UseCases.Publicadores.ListTodos;

public class ListPublicadoresHandler(IAppDbContext context)
    : IRequestHandler<ListPublicadoresQuery, List<PublicadorDetalheDto>>
{
    public async Task<List<PublicadorDetalheDto>> Handle(ListPublicadoresQuery request, CancellationToken cancellationToken)
    {
        return await context.Publicadores
            .OrderBy(p => p.Nome)
            .Select(p => new PublicadorDetalheDto(
                p.Id,
                p.Nome,
                p.EmailUsername,
                p.Sexo.ToString(),
                p.DataBatismo != null,
                p.DataNascimento,
                p.DataBatismo,
                p.Telefone,
                p.Endereco,
                p.Cargos.Where(c => c.DataFim == null).Select(c => (int?)c.IdCargo).FirstOrDefault(),
                p.Cargos.Where(c => c.DataFim == null).Select(c => c.Cargo.NomeCargo).FirstOrDefault(),
                p.Cargos.Where(c => c.DataFim == null).Select(c => (DateOnly?)c.DataInicio).FirstOrDefault(),
                p.Privilegios.Where(v => v.DataFim == null).Select(v => (int?)v.IdPrivilegio).FirstOrDefault(),
                p.Privilegios.Where(v => v.DataFim == null).Select(v => v.Privilegio.NomePrivilegio).FirstOrDefault(),
                p.Privilegios.Where(v => v.DataFim == null).Select(v => (DateOnly?)v.DataInicio).FirstOrDefault(),
                p.Grupos.Select(gp => (int?)gp.IdGrupo).FirstOrDefault(),
                p.Grupos.Select(gp => gp.Grupo.Nome).FirstOrDefault()
            ))
            .ToListAsync(cancellationToken);
    }
}
