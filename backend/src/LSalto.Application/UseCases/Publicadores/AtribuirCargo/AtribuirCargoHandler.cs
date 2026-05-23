using LSalto.Application.Common.Interfaces;
using LSalto.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Application.UseCases.Publicadores.AtribuirCargo;

public class AtribuirCargoHandler(IAppDbContext context) : IRequestHandler<AtribuirCargoCommand>
{
    public async Task Handle(AtribuirCargoCommand request, CancellationToken cancellationToken)
    {
        // Encerra cargo ativo anterior
        var ativo = await context.PublicadoresCargos
            .Where(c => c.IdPublicador == request.IdPublicador && c.DataFim == null)
            .FirstOrDefaultAsync(cancellationToken);

        if (ativo != null)
            ativo.DataFim = request.DataInicio.AddDays(-1);

        context.PublicadoresCargos.Add(new PublicadorCargo
        {
            IdPublicador = request.IdPublicador,
            IdCargo = request.IdCargo,
            DataInicio = request.DataInicio,
            DataFim = null
        });

        await context.SaveChangesAsync(cancellationToken);
    }
}
