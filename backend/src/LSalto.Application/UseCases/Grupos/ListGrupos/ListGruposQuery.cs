using LSalto.Application.DTOs;
using MediatR;

namespace LSalto.Application.UseCases.Grupos.ListGrupos;

public record ListGruposQuery : IRequest<List<GrupoDto>>;
