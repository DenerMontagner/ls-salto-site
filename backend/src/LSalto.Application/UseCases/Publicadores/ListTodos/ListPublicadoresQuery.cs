using LSalto.Application.DTOs;
using MediatR;

namespace LSalto.Application.UseCases.Publicadores.ListTodos;

public record ListPublicadoresQuery : IRequest<List<PublicadorDetalheDto>>;
