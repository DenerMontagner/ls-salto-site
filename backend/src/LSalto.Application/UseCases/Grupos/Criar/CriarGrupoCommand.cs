using MediatR;

namespace LSalto.Application.UseCases.Grupos.Criar;

public record CriarGrupoCommand(string Nome, string? Local, int? IdAnciaoResponsavel) : IRequest<int>;
