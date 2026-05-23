using LSalto.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Application.UseCases.Publicadores.RemoverCargo;

public class RemoverCargoHandler(IAppDbContext context) : IRequestHandler<RemoverCargoCommand>
{
    public async Task Handle(RemoverCargoCommand request, CancellationToken cancellationToken)
    {
        var ativo = await context.PublicadoresCargos
            .Where(c => c.IdPublicador == request.IdPublicador && c.DataFim == null)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new InvalidOperationException("Este publicador não possui cargo ativo.");

        ativo.DataFim = DateOnly.FromDateTime(DateTime.Today);
        await context.SaveChangesAsync(cancellationToken);
    }
}
