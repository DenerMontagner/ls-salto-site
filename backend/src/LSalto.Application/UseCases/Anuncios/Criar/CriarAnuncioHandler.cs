using LSalto.Application.Common.Interfaces;
using LSalto.Domain.Entities;
using MediatR;

namespace LSalto.Application.UseCases.Anuncios.Criar;

public class CriarAnuncioHandler(IAppDbContext context) : IRequestHandler<CriarAnuncioCommand, int>
{
    public async Task<int> Handle(CriarAnuncioCommand request, CancellationToken cancellationToken)
    {
        var anuncio = new Anuncio { Descricao = request.Descricao };
        context.Anuncios.Add(anuncio);
        await context.SaveChangesAsync(cancellationToken);
        return anuncio.Id;
    }
}
