using MediatR;

namespace LSalto.Application.UseCases.Designacoes.Escalonar;

public record DesignacaoInputDto(
    int IdTipoDesignacao,
    DateOnly Data,
    int IdPublicadorTitular,
    int? IdPublicadorAjudante,
    int? IdGrupo
);

public record EscalonarMesCommand(
    int Ano,
    int Mes,
    List<DesignacaoInputDto> Designacoes
) : IRequest<int>;
