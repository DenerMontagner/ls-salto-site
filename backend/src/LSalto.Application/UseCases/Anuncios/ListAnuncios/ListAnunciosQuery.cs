using LSalto.Application.DTOs;
using MediatR;

namespace LSalto.Application.UseCases.Anuncios.ListAnuncios;

public record ListAnunciosQuery : IRequest<List<AnuncioComAnexosDto>>;
