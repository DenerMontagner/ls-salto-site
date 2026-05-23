using LSalto.Application.Common.Interfaces;
using LSalto.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Application.UseCases.Publicadores.AtribuirPrivilegio;

public class AtribuirPrivilegioHandler(IAppDbContext context) : IRequestHandler<AtribuirPrivilegioCommand>
{
    public async Task Handle(AtribuirPrivilegioCommand request, CancellationToken cancellationToken)
    {
        var ativo = await context.PublicadoresPrivilegios
            .Where(v => v.IdPublicador == request.IdPublicador && v.DataFim == null)
            .FirstOrDefaultAsync(cancellationToken);

        if (ativo != null)
            ativo.DataFim = request.DataInicio.AddDays(-1);

        context.PublicadoresPrivilegios.Add(new PublicadorPrivilegio
        {
            IdPublicador = request.IdPublicador,
            IdPrivilegio = request.IdPrivilegio,
            DataInicio = request.DataInicio,
            DataFim = null
        });

        await context.SaveChangesAsync(cancellationToken);
    }
}
