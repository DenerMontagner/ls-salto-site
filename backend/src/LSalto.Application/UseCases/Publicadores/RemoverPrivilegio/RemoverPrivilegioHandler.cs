using LSalto.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Application.UseCases.Publicadores.RemoverPrivilegio;

public class RemoverPrivilegioHandler(IAppDbContext context) : IRequestHandler<RemoverPrivilegioCommand>
{
    public async Task Handle(RemoverPrivilegioCommand request, CancellationToken cancellationToken)
    {
        var ativo = await context.PublicadoresPrivilegios
            .Where(v => v.IdPublicador == request.IdPublicador && v.DataFim == null)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new InvalidOperationException("Este publicador não possui privilégio ativo.");

        ativo.DataFim = DateOnly.FromDateTime(DateTime.Today);
        await context.SaveChangesAsync(cancellationToken);
    }
}
