using MediatR;

namespace LSalto.Application.UseCases.Designacoes.Criar;

public record CriarDesignacaoCommand(
    int IdTipoDesignacao,
    DateOnly Data,
    int IdPublicadorTitular,
    int? IdPublicadorAjudante,
    int? IdGrupo
) : IRequest<int>;
