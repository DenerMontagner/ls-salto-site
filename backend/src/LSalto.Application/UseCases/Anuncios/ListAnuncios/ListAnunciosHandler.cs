using LSalto.Application.Common.Interfaces;
using LSalto.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LSalto.Application.UseCases.Anuncios.ListAnuncios;

public class ListAnunciosHandler(IAppDbContext context)
    : IRequestHandler<ListAnunciosQuery, List<AnuncioComAnexosDto>>
{
    public async Task<List<AnuncioComAnexosDto>> Handle(ListAnunciosQuery request, CancellationToken cancellationToken)
    {
        return await context.Anuncios
            .Include(a => a.Anexos)
            .OrderByDescending(a => a.DataCriacao)
            .Select(a => new AnuncioComAnexosDto(
                a.Id,
                a.Descricao,
                a.DataCriacao,
                a.Anexos.Select(x => x.CaminhoArquivoUrl).ToList()
            ))
            .ToListAsync(cancellationToken);
    }
}
