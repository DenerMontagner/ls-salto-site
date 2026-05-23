using LSalto.Application.Common.Interfaces;
using LSalto.Domain.Entities;
using MediatR;

namespace LSalto.Application.UseCases.Grupos.Criar;

public class CriarGrupoHandler(IAppDbContext context) : IRequestHandler<CriarGrupoCommand, int>
{
    public async Task<int> Handle(CriarGrupoCommand request, CancellationToken cancellationToken)
    {
        var grupo = new Grupo
        {
            Nome = request.Nome,
            Local = request.Local,
            IdAnciaoResponsavel = request.IdAnciaoResponsavel
        };

        context.Grupos.Add(grupo);
        await context.SaveChangesAsync(cancellationToken);
        return grupo.Id;
    }
}
