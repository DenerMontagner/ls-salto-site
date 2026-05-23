using LSalto.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Application.UseCases.Publicadores.Excluir;

public class ExcluirPublicadorHandler(IAppDbContext context)
    : IRequestHandler<ExcluirPublicadorCommand>
{
    public async Task Handle(ExcluirPublicadorCommand request, CancellationToken cancellationToken)
    {
        var publicador = await context.Publicadores
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Publicador {request.Id} não encontrado.");

        context.Publicadores.Remove(publicador);
        await context.SaveChangesAsync(cancellationToken);
    }
}
