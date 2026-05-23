using LSalto.Application.DTOs;
using MediatR;

namespace LSalto.Application.UseCases.Publicadores.ListPorTipo;

public record ListPublicadoresPorTipoQuery(int IdTipoDesignacao) : IRequest<List<PublicadorResumoDto>>;
